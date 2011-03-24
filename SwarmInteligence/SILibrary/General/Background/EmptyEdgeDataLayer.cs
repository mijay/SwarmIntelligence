using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General.Background
{
    public class EmptyEdgeDataLayer<C>: DelegateEdgeDataLayer<C, EmptyData>
        where C: ICoordinate<C>
    {
        public EmptyEdgeDataLayer(Topology<C> topology)
            : base(topology, delegate { return EmptyData.Instance; }) {}
    }
}