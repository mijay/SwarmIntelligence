using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SwarmIntelligence2.Core.Commands;
using SwarmIntelligence2.Core.Coordinates;
using Utils;

namespace SwarmIntelligence2.Core
{
    public class Runner<C, B>
        where C: ICoordinate<C>
    {
        private readonly Background<C, B> background;
        private readonly Func<CommandEvaluator<C, B>> commandEvaluatorFactory;
        private readonly Map<C, B> map;

        public Runner(Map<C, B> map, Background<C, B> background, Func<CommandEvaluator<C, B>> commandEvaluatorFactory)
        {
            this.map = map;
            this.background = background;
            this.commandEvaluatorFactory = commandEvaluatorFactory;
        }

        private ThreadLocal<CommandEvaluator<C, B>> CommandEvaluator
        {
            get
            {
                return new ThreadLocal<CommandEvaluator<C, B>>(
                    () => {
                        CommandEvaluator<C, B> result = commandEvaluatorFactory();
                        result.EvaluationContext = new EvaluationContext<C, B> { Map = map, Background = background };
                        return result;
                    });
            }
        }

        public void ProcessTurn()
        {
            AntContext[] obtainedCommands = ObtainCommands();
            ExecuteCommands(obtainedCommands);
        }

        private void ExecuteCommands(AntContext[] obtainedCommands)
        {
            ThreadLocal<CommandEvaluator<C, B>> loclEvaluator = CommandEvaluator;
            obtainedCommands
                //.AsParallel()
                .ForEach(commandsInContext => {
                             CommandEvaluator<C, B> commandEvaluator = loclEvaluator.Value;
                             commandsInContext.CopyTo(commandEvaluator.EvaluationContext);
                             commandsInContext.commands.ForEach(command => command.Visit(commandEvaluator));
                         });
        }

        private AntContext[] ObtainCommands()
        {
            return GetAntContexts()
                .Select(context => {
                            context.commands = context.ant.ProcessTurn();
                            return context;
                        })
                .Where(context => !context.commands.IsEmpty())
                .ToArray();
        }

        private ParallelQuery<AntContext> GetAntContexts()
        {
            return map
                .GetExistenData()
                .AsParallel()
                .SelectMany(cellWithCoord => cellWithCoord.Value
                                                 .Select(ant => new AntContext
                                                                {
                                                                    coord = cellWithCoord.Key,
                                                                    cell = cellWithCoord.Value,
                                                                    ant = ant
                                                                }));
        }

        #region Nested type: AntContext

        private struct AntContext
        {
            public Ant<C, B> ant;
            public Cell<C, B> cell;
            public IEnumerable<Command<C, B>> commands;
            public C coord;

            public void CopyTo(EvaluationContext<C, B> evaluationContext)
            {
                evaluationContext.Ant = ant;
                evaluationContext.Cell = cell;
                evaluationContext.Coordinate = coord;
            }
        }

        #endregion
    }
}