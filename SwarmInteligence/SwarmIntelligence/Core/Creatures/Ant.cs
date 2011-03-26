using System.Collections.Generic;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Core.Creatures
{
    public abstract class Ant<C, B, E>
        where C: ICoordinate<C>
    {
        public abstract IEnumerable<Command<C, B, E>> ProcessTurn();
    }
}