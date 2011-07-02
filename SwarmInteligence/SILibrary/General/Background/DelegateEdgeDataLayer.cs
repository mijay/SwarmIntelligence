using System;
using SwarmIntelligence.Core.DataLayer;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General.Background
{
    public class DelegateEdgeDataLayer<C, E>: EdgeDataLayer<C, E>
        where C: ICoordinate<C>
    {
        private readonly Func<Edge<C>, E> factory;

        public DelegateEdgeDataLayer(Topology<C> topology, Func<Edge<C>, E> factory): base(topology)
        {
            this.factory = factory;
        }

        #region Overrides of EdgeDataLayer<C,E>

        public override E this[Edge<C> key]
        {
            get { return factory(key); }
        }

        #endregion
    }
}