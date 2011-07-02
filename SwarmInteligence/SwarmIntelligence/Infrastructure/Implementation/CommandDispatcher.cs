using System.Collections.Generic;
using System.Linq;
using Common.Cache;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Implementation
{
    public class CommandDispatcher<C, B, E>: ICommandDispatcher<C, B, E>
        where C: ICoordinate<C>
    {
        private readonly IEnumerable<ITypedCommandDispatcher<C, B, E>> commandDispatchers;
        private readonly IKeyValueCache keyValueCache;

        public CommandDispatcher(IEnumerable<ITypedCommandDispatcher<C, B, E>> commandDispatchers, IKeyValueCache keyValueCache)
        {
            this.commandDispatchers = commandDispatchers;
            this.keyValueCache = keyValueCache;
        }

        #region ICommandDispatcher<C,B,E> Members

        public void Dispatch<TCommand>(TCommand command) where TCommand: Command<C, B, E>
        {
            keyValueCache
                .GetOrAdd(typeof(TCommand),
                          delegate {
                              return commandDispatchers
                                  .OfType<ITypedCommandDispatcher<TCommand, C, B, E>>()
                                  .Single();
                          })
                .Dispatch(command);
        }

        #endregion
    }
}