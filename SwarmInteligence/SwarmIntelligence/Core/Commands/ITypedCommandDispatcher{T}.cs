namespace SwarmIntelligence.Core.Commands
{
    public interface ITypedCommandDispatcher<in TCommand>: ITypedCommandDispatcher
    {
        void Dispatch(TCommand command);
    }
}