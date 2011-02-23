using System;
using SwarmIntelligence2.Core;
using SwarmIntelligence2.Core.Coordinates;
using Utils.Cache;

namespace SwarmIntelligence2.GeneralImplementation
{
    public class CachedBackground<C, B>: Background<C, B>
        where C: struct, ICoordinate<C>
    {
        private readonly Func<C, B> cachedGet;

        public CachedBackground(Background<C, B> background, IFuncCacher funcCacher): base(background.Range)
        {
            cachedGet = funcCacher.MakeCached<C, B>(c => background[c]);
        }

        #region Overrides of Background<C,B>

        public override B this[C coord]
        {
            get { return cachedGet(coord); }
        }

        #endregion
    }
}