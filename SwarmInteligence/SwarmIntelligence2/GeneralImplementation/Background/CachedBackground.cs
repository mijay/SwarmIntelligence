using System;
using SwarmIntelligence2.Core.World;
using SwarmIntelligence2.Core.World.Data;
using Utils.Cache;

namespace SwarmIntelligence2.GeneralImplementation.Background
{
    public class CachedBackground<C, B>: Background<C, B>
        where C: ICoordinate<C>
    {
        private readonly Func<C, B> cachedGet;

        public CachedBackground(Background<C, B> background, IMemoizer memoizer): base(background.Boundaries)
        {
            cachedGet = memoizer.Memoize<C, B>(c => background[c]);
        }

        #region Overrides of Background<C,B>

        public override B this[C key]
        {
            get { return cachedGet(key); }
        }

        #endregion
    }
}