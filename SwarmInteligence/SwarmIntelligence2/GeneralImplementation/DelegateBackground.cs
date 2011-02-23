using System;
using SwarmIntelligence2.Core;
using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.GeneralImplementation
{
    public class DelegateBackground<C, B>: Background<C, B>
        where C: struct, ICoordinate<C>
    {
        private readonly Func<C, B> factory;

        public DelegateBackground(Range<C> range, Func<C, B> factory): base(range)
        {
            this.factory = factory;
        }

        #region Overrides of Background<C,B>

        public override B this[C coord]
        {
            get { return factory(coord); }
        }

        #endregion
    }
}