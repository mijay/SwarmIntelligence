using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public interface ITypedCommandDispatcher<C, B, E>
        where C: ICoordinate<C> {}
}