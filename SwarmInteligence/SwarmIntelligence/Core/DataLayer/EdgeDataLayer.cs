using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.DataLayer
{
    [ContractClass(typeof(ContractEdgeDataLayer<,>))]
    public abstract class EdgeDataLayer<C, E>: IMapping<Edge<C>, E>
        where C: ICoordinate<C>
    {
        protected EdgeDataLayer(Topology<C> topology)
        {
            Topology = topology;
        }

        #region Implementation of IMapping<in C,out E>

        public abstract E this[Edge<C> key] { get; }

        #endregion

        public Topology<C> Topology { get; private set; }
    }

    [ContractClassFor(typeof(EdgeDataLayer<,>))]
    internal abstract class ContractEdgeDataLayer<C, E>: EdgeDataLayer<C, E>
        where C: ICoordinate<C>
    {
        protected ContractEdgeDataLayer(Topology<C> topology): base(topology) {}

        public override E this[Edge<C> key]
        {
            get
            {
                Contract.Requires(Topology.Exists(key));

                throw new NotImplementedException();
            }
        }
    }
}