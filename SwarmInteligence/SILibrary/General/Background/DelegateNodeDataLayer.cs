using System;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General.Background
{
    public class DelegateNodeDataLayer<C, B>: NodeDataLayer<C, B>
        where C: ICoordinate<C>
    {
        private readonly Func<C, B> factory;

        public DelegateNodeDataLayer(Boundaries<C> boundaries, Func<C, B> factory): base(boundaries)
        {
            this.factory = factory;
        }

        #region Overrides of NodeDataLayer<C,B>

        public override B this[C key]
        {
            get { return factory(key); }
        }

        #endregion
    }
}