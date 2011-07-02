using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Collections;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Implementation
{
    public class Runner<C, B, E>
        where C: ICoordinate<C>
    {
        private readonly ICommandDispatcher<C, B, E> commandDispatcher;
        private readonly World<C, B, E> world;

        public Runner(World<C, B, E> world, ICommandDispatcher<C, B, E> commandDispatcher)
        {
            this.world = world;
            this.commandDispatcher = commandDispatcher;
        }

        public void ProcessTurn()
        {
            ExecuteCommands(ObtainCommands());
        }

        private void ExecuteCommands(IEnumerable<Command<C, B, E>> obtainedCommands)
        {
            obtainedCommands.ForEach(command => command.Dispatch(commandDispatcher));
        }

        private Command<C, B, E>[] ObtainCommands()
        {
            CommandContext<C, B, E>.CurrentContext =
                new ThreadLocal<CommandContext<C, B, E>>(() => new CommandContext<C, B, E> { World = world });

            return GetAntContexts()
                .SelectMany(context => {
                                InitializeCommandContext(context);
                                return context.ant.ProcessTurn();
                            })
                .ToArray();
        }

        private ParallelQuery<AntContext> GetAntContexts()
        {
            return world.Map
                .GetInitialized()
                .AsParallel()
                .SelectMany(cellWithCoord =>
                            cellWithCoord.Value
                                .Select(ant => new AntContext
                                               {
                                                   coord = cellWithCoord.Key,
                                                   ant = ant
                                               }))
                .AsParallel();
        }

        private static void InitializeCommandContext(AntContext context)
        {
            CommandContext<C, B, E>.CurrentContext.Value.Ant = context.ant;
            CommandContext<C, B, E>.CurrentContext.Value.Coordinate = context.coord;
        }

        #region Nested type: AntContext

        private struct AntContext
        {
            public Ant<C, B, E> ant;
            public C coord;
        }

        #endregion
    }
}