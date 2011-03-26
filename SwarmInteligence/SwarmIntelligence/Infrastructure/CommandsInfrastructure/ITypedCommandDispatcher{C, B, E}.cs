using SwarmIntelligence.Core;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public interface ITypedCommandDispatcher<C, B, E>
        where C: ICoordinate<C> {}
}