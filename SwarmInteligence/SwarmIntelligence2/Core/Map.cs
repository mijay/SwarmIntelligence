using System.Collections.Generic;
using SwarmIntelligence2.Core.Coordinates;
using SwarmIntelligence2.Core.World;
using SwarmIntelligence2.Core.World.Data;

namespace SwarmIntelligence2.Core
{
    public abstract class Map<C, B>: ILazyMapping<C, Cell<C, B>>
        where C: ICoordinate<C>
    {
        protected Map(Range<C> range)
        {
            Range = range;
        }

        #region Implementation of IMapping<C,out Cell<C,B>>

        public Range<C> Range { get; private set; }
        public abstract Cell<C, B> this[C key] { get; }

        #endregion

        #region Implementation of ILazyMapping<C,out Cell<C,B>>

        public abstract bool IsInitialized(C key);
        public abstract void Free(C key);
        public abstract IEnumerable<KeyValuePair<C, Cell<C, B>>> GetInitialized();

        #endregion
    }
}