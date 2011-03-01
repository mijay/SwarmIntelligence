using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SwarmIntelligence2.Core.Commands;
using SwarmIntelligence2.Core.Coordinates;
using SwarmIntelligence2.GeneralImplementation;
using Utils;

namespace SwarmIntelligence2.Core
{
    public class Runner<C, B>
        where C: ICoordinate<C>
    {
        private readonly Background<C, B> background;
        private readonly Map<C, B> map;

        public Runner(Map<C, B> map, Background<C, B> background)
        {
            this.map = map;
            this.background = background;
        }

        public void ProcessTurn()
        {
            AntContext[] obtainedCommands = ObtainCommands();
            ExecuteCommands(obtainedCommands);
        }

        private void ExecuteCommands(IEnumerable<AntContext> obtainedCommands)
        {
            var localContext = new ThreadLocal<EvaluationContext<C, B>>(
                () => new EvaluationContext<C, B> { Map = map, Background = background });
            obtainedCommands
                .ForEach(commandsInContext => {
                             var evaluationContext = localContext.Value;
                             commandsInContext.CopyTo(evaluationContext);
                             commandsInContext.commands.ForEach(command => command.Evaluate(evaluationContext));
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