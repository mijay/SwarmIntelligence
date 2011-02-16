using System.Collections.Generic;
using SwarmIntelligence2.Core.Commands;
using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.Core
{
    public abstract class Ant<C, B>
        where C: struct, ICoordinate<C>
    {
        public abstract IEnumerable<Command<C, B>> ProcessTurn();
    }
}