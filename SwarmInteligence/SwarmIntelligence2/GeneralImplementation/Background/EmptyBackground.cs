using SwarmIntelligence2.Core.World;
using SwarmIntelligence2.Core.World.Space;

namespace SwarmIntelligence2.GeneralImplementation.Background
{
    public class EmptyBackground<C>: DelegateBackground<C, EmptyData>
        where C: ICoordinate<C>
    {
        public EmptyBackground(Boundaries<C> boundaries)
            : base(boundaries, delegate { return EmptyData.Instance; }) {}
    }
}