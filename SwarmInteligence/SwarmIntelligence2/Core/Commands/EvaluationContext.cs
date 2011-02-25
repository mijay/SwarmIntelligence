using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands
{
    public class EvaluationContext<C, B>
        where C: ICoordinate<C>
    {
        public Map<C, B> Map { get; set; }
        public C Coordinate { get; set; }
        public Ant<C, B> Ant { get; set; }
        public Cell<C, B> Cell { get; set; }
        //Suburb
    }
}