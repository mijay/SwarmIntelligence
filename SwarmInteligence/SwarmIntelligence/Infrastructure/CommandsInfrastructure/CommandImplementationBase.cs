using SwarmIntelligence.Core;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public abstract class CommandImplementationBase<C, B, E>: Command<C, B, E>
        where C: ICoordinate<C>
    {
        public CommandContext<C, B, E> Context { get; private set; }

        protected CommandImplementationBase()
        {
            Context = CommandContext<C,B,E>.CurrentContext.Value.Clone();
        }
    }
}