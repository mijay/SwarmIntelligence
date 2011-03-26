﻿using System.Collections.Generic;
using System.Linq;
using Common;
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
            CommandContext<C, B, E>.CurrentContext = new CommandContext<C, B, E>
                                                     {
                                                         World = world
                                                     };
            obtainedCommands
                .ForEach(commandsInContext => {
                             CommandContext<C, B, E>.CurrentContext.Ant = commandsInContext.ant;
                             CommandContext<C, B, E>.CurrentContext.Coordinate = commandsInContext.coord;

                             commandsInContext.commands.ForEach(command => commandDispatcher.Dispatch(command));
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
            return world
                .Map
                .GetInitialized()
                .AsParallel()
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