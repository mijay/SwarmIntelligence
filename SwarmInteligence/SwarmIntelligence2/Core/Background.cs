using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.Core
{
    public abstract class Background<C, B>: ICoordinateMapping<C, B>
        where C: struct, ICoordinate<C>
    {
        protected Background(Range<C> range)
        {
            Range = range;
        }

        #region Implementation of ICoordinateMapping<C,out B>

        public Range<C> Range { get; private set; }

        public abstract B this[C coord] { get; }

        #endregion
    }
}