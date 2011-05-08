using SwarmIntelligence.Core;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public abstract class CommandImplementationBase<C, B, E>: Command<C, B, E>
        where C: ICoordinate<C>
    {
        protected CommandImplementationBase()
        {
            Context = CommandContext<C, B, E>.CurrentContext.Value.Clone();
        }

        public CommandContext<C, B, E> Context { get; private set; }
    }
}