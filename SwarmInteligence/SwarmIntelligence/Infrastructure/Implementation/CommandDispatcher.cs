using System.Collections.Generic;
using System.Linq;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Implementation
{
    public class CommandDispatcher<C, B, E>: ICommandDispatcher<C, B, E>
        where C: ICoordinate<C>
    {
        private readonly IEnumerable<ITypedCommandDispatcher<C, B, E>> commandDispatchers;

        public CommandDispatcher(IEnumerable<ITypedCommandDispatcher<C, B, E>> commandDispatchers)
        {
            this.commandDispatchers = commandDispatchers;
        }

        #region ICommandDispatcher<C,B,E> Members

        public void Dispatch(Command<C, B, E> command)
        {
            commandDispatchers
                .OfType<ITypedCommandDispatcher<Command<C, B, E>, C, B, E>>()
                .Single()
                .Dispatch(command);
        }

        #endregion
    }
}