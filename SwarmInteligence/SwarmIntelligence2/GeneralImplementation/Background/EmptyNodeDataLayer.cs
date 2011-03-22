using SwarmIntelligence2.Core;
using SwarmIntelligence2.Core.Space;

namespace SwarmIntelligence2.GeneralImplementation.Background
{
    public class EmptyNodeDataLayer<C>: DelegateNodeDataLayer<C, EmptyData>
        where C: ICoordinate<C>
    {
        public EmptyNodeDataLayer(Boundaries<C> boundaries)
            : base(boundaries, delegate { return EmptyData.Instance; }) {}
    }
}