using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public interface ICommandDispatcher
    {
        void Dispatch<C, B, E>(Command<C, B, E> command) where C: ICoordinate<C>;
    }
}