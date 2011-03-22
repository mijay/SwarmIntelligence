using SwarmIntelligence2.Core.World;
using SwarmIntelligence2.Core.World.Data;

namespace SwarmIntelligence2.Core.Commands
{
    public class EvaluationContext<C, B>
        where C: ICoordinate<C>
    {
        public Ant<C, B> Ant;
        public Background<C, B> Background;
        public Cell<C, B> Cell;
        public C Coordinate;
        public Map<C, B> Map;
        //Suburb
    }
}