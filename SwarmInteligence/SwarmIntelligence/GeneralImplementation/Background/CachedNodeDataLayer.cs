using System;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Data;
using Utils.Cache;

namespace SwarmIntelligence.GeneralImplementation.Background
{
    public class CachedNodeDataLayer<C, B>: NodeDataLayer<C, B>
        where C: ICoordinate<C>
    {
        private readonly Func<C, B> cachedGet;

        public CachedNodeDataLayer(NodeDataLayer<C, B> nodeDataLayer, IMemoizer memoizer): base(nodeDataLayer.Boundaries)
        {
            cachedGet = memoizer.Memoize<C, B>(c => nodeDataLayer[c]);
        }

        #region Overrides of NodeDataLayer<C,B>

        public override B this[C key]
        {
            get { return cachedGet(key); }
        }

        #endregion
    }
}