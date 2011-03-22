namespace SwarmIntelligence.Core.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch<C, B, E>(Command<C, B, E> command) where C: ICoordinate<C>;
    }
}