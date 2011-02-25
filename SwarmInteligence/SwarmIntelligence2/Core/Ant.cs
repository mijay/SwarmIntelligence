using System.Collections.Generic;
using SwarmIntelligence2.Core.Commands;
using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core
{
    public abstract class Ant<C, B>
        where C: ICoordinate<C>
    {
        public abstract IEnumerable<Command<C, B>> ProcessTurn();
    }
}