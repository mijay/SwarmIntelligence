using SwarmIntelligence.Core;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public interface ITypedCommandDispatcher<in TCommand, C, B, E>: ITypedCommandDispatcher<C, B, E>
        where TCommand: Command<C, B, E>
        where C: ICoordinate<C>
    {
        void Dispatch(TCommand command);
    }
}