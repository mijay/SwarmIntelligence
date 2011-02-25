using System.Collections.Generic;
using SwarmIntelligence2.Core.Coordinates;
using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.Core
{
    public abstract class Map<C, B>: IIncompleteCoordinateMapping<C, Cell<C, B>>
        where C: ICoordinate<C>
    {
        protected Map(Range<C> range)
        {
            Range = range;
        }

        #region Implementation of ICoordinateMapping<C,out Cell<C,B>>

        public Range<C> Range { get; private set; }
        public abstract Cell<C, B> this[C coord] { get; }

        #endregion

        #region Implementation of IIncompleteCoordinateMapping<C,out Cell<C,B>>

        public abstract bool HasData(C coord);
        public abstract void ClearData(C coord);
        public abstract IEnumerable<KeyValuePair<C, Cell<C, B>>> GetExistenData();

        #endregion
    }
}