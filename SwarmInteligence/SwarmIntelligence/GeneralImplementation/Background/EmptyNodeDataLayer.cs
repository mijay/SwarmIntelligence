using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.GeneralImplementation.Background
{
    public class EmptyNodeDataLayer<C>: DelegateNodeDataLayer<C, EmptyData>
        where C: ICoordinate<C>
    {
        public EmptyNodeDataLayer(Boundaries<C> boundaries)
            : base(boundaries, delegate { return EmptyData.Instance; }) {}
    }
}