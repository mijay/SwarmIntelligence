using System.Collections.Generic;
using System.Linq;
using Common.Collections;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Implementation
{
    public class Runner<C, B, E>
        where C: ICoordinate<C>
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly World<C, B, E> world;

        public Runner(World<C, B, E> world, ICommandDispatcher commandDispatcher)
        {
            this.world = world;
            this.commandDispatcher = commandDispatcher;
        }

        public void ProcessTurn()
        {
            AntContext[] obtainedCommands = ObtainCommands();
            ExecuteCommands(obtainedCommands);
        }

        private void ExecuteCommands(IEnumerable<AntContext> obtainedCommands)
        {
            obtainedCommands.ForEach(
                commandsInContext => commandsInContext.commands.ForEach(command => commandDispatcher.Dispatch(command)));
        }

        private AntContext[] ObtainCommands()
        {
            return GetAntContexts()
                .Select(context => {
                            InitializeCommandContext(context);
                            context.commands = context.ant.ProcessTurn();
                            return context;
                        })
                .Where(context => !context.commands.IsEmpty())
                .ToArray();
        }

        private void InitializeCommandContext(AntContext context)
        {
            if(CommandContext<C, B, E>.CurrentContext == null)
                CommandContext<C, B, E>.CurrentContext = new CommandContext<C, B, E> { World = world };
            CommandContext<C, B, E>.CurrentContext.Ant = context.ant;
            CommandContext<C, B, E>.CurrentContext.Coordinate = context.coord;
        }

        //private ParallelQuery<AntContext> GetAntContexts()
        private IEnumerable<AntContext> GetAntContexts()
        {
            return world
                .Map
                .GetInitialized()
                //  .AsParallel()
                .SelectMany(cellWithCoord => cellWithCoord.Value
                                                 .Select(ant => new AntContext
                                                                {
                                                                    coord = cellWithCoord.Key,
                                                                    ant = ant
                                                                }));
        }

        #region Nested type: AntContext

        private struct AntContext
        {
            public Ant<C, B, E> ant;
            public IEnumerable<Command<C, B, E>> commands;
            public C coord;
        }

        #endregion
    }
}