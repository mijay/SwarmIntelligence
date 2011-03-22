using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Commands;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Core.Data;
using Utils;

namespace SwarmIntelligence.Implementation
{
    public class Runner<C, B, E>
        where C: ICoordinate<C>
    {
        private readonly Map<C, B, E> map;
        private readonly NodeDataLayer<C, B> nodeDataLayer;

        public Runner(Map<C, B, E> map, NodeDataLayer<C, B> nodeDataLayer)
        {
            this.map = map;
            this.nodeDataLayer = nodeDataLayer;
        }

        public void ProcessTurn()
        {
            AntContext[] obtainedCommands = ObtainCommands();
            ExecuteCommands(obtainedCommands);
        }

        private void ExecuteCommands(IEnumerable<AntContext> obtainedCommands)
        {
            var localContext = new ThreadLocal<EvaluationContext<C, B, E>>(
                () => new EvaluationContext<C, B, E> { Map = map, NodeDataLayer = nodeDataLayer });
            obtainedCommands
                .ForEach(commandsInContext => {
                             EvaluationContext<C, B, E> evaluationContext = localContext.Value;
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
                .GetInitialized()
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
            public Ant<C, B, E> ant;
            public Cell<C, B, E> cell;
            public IEnumerable<Command<C, B, E>> commands;
            public C coord;

            public void CopyTo(EvaluationContext<C, B, E> evaluationContext)
            {
                evaluationContext.Ant = ant;
                evaluationContext.Cell = cell;
                evaluationContext.Coordinate = coord;
            }
        }

        #endregion
    }
}