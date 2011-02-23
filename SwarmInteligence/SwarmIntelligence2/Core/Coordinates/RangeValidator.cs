using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SwarmIntelligence2.Core.Coordinates
{
    public abstract class RangeValidator<C>: IComparer<C>
        where C: struct, ICoordinate<C>
    {
        [Pure]
        public static RangeValidator<C> Instance { get; private set; }

        protected static void Register(RangeValidator<C> validator)
        {
            Contract.Requires<InvalidOperationException>(Instance == null);
            Contract.Requires<ArgumentNullException>(validator != null);
            Contract.Ensures(Instance != null);

            Instance = validator;
        }

        #region Implementation of IComparer<in C>

        [Pure]
        public abstract int Compare(C x, C y);

        #endregion

        [Pure]
        public bool IsInRange(Range<C> range, C coord)
        {
            return LessOrEqual(range.min, coord) && LessOrEqual(coord, range.max);
        }

        [Pure]
        public bool LessOrEqual(C x, C y)
        {
            return Compare(x, y) <= 0;
        }
    }
}