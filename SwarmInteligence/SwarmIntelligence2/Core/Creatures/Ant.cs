using System.Collections.Generic;
using SwarmIntelligence2.Core.Commands;

namespace SwarmIntelligence2.Core.Creatures
{
    public abstract class Ant<C, B, E>
        where C: ICoordinate<C>
    {
        public abstract IEnumerable<Command<C, B, E>> ProcessTurn();
    }
}