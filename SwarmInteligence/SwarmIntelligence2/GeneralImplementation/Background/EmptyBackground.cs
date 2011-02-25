using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.GeneralImplementation.Background
{
    public class EmptyBackground<C>: DelegateBackground<C, EmptyData>
        where C: ICoordinate<C>
    {
        public EmptyBackground(Range<C> range)
            : base(range, delegate { return EmptyData.Instance; }) {}
    }
}