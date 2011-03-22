using System;
using SwarmIntelligence2.Core.World;
using SwarmIntelligence2.Core.World.Data;
using SwarmIntelligence2.Core.World.Space;

namespace SwarmIntelligence2.GeneralImplementation.Background
{
    public class DelegateBackground<C, B>: Background<C, B>
        where C: ICoordinate<C>
    {
        private readonly Func<C, B> factory;

        public DelegateBackground(Boundaries<C> boundaries, Func<C, B> factory): base(boundaries)
        {
            this.factory = factory;
        }

        #region Overrides of Background<C,B>

        public override B this[C key]
        {
            get { return factory(key); }
        }

        #endregion
    }
}