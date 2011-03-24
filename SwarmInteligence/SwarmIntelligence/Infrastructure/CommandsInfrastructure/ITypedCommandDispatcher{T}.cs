namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public interface ITypedCommandDispatcher<in TCommand>: ITypedCommandDispatcher
    {
        void Dispatch(TCommand command);
    }
}