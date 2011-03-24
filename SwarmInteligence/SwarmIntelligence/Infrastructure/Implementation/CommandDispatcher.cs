using System.Collections.Generic;
using System.Linq;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Implementation
{
    public class CommandDispatcher: ICommandDispatcher
    {
        private readonly IEnumerable<ITypedCommandDispatcher> commandDispatchers;

        public CommandDispatcher(IEnumerable<ITypedCommandDispatcher> commandDispatchers)
        {
            this.commandDispatchers = commandDispatchers;
        }

        #region Implementation of ICommandDispatcher

        public void Dispatch<C, B, E>(Command<C, B, E> command) where C: ICoordinate<C>
        {
            commandDispatchers
                .OfType<ITypedCommandDispatcher<Command<C, B, E>>>()
                .Single()
                .Dispatch(command);
        }

        #endregion
    }
}