using SwarmIntelligence.Core;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Commands
{
    public class SelfRemove<C, B, E>: CommandImplementationBase<C, B, E>
        where C: ICoordinate<C> {}
}