using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Data
{
    [ContractClass(typeof(ContractEdgeDataLayer<,>))]
    public abstract class EndgeDataLayer<C, E>: IMapping<Edge<C>, E>
        where C: ICoordinate<C>
    {
        protected EndgeDataLayer(Topology<C> topology)
        {
            Topology = topology;
        }

        #region Implementation of IMapping<in C,out E>

        public abstract E this[Edge<C> key] { get; }

        #endregion

        public Topology<C> Topology { get; private set; }
    }

    [ContractClassFor(typeof(EndgeDataLayer<,>))]
    internal abstract class ContractEdgeDataLayer<C, E>: EndgeDataLayer<C, E>
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