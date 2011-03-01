using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands
{
    public class EvaluationContext<C, B>
        where C: ICoordinate<C>
    {
        public Background<C, B> Background;
        public Map<C, B> Map;
        public C Coordinate;
        public Ant<C, B> Ant;
        public Cell<C, B> Cell;
        //Suburb
    }
}