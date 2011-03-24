using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public abstract class CommandImplementationBase<C, B, E>: Command<C, B, E>
        where C: ICoordinate<C>
    {
        public CommandContext<C, B, E> CommandContext { get; private set; }

        protected CommandImplementationBase()
        {
            CommandContext = CommandContext<C,B,E>.CurrentContext.Clone();
        }
    }
}