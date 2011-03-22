using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Core.Data;

namespace SwarmIntelligence.Core.Commands
{
    public class EvaluationContext<C, B, E>
        where C: ICoordinate<C>
    {
        public Ant<C, B, E> Ant;
        public Cell<C, B, E> Cell;
        public C Coordinate;
        public Map<C, B, E> Map;
        public NodeDataLayer<C, B> NodeDataLayer;
        //Suburb
    }
}