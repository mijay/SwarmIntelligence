using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.DataLayer
{
    [ContractClass(typeof(ContractNodeDataLayer<,>))]
    public abstract class NodeDataLayer<C, B>: IMapping<C, B>
        where C: ICoordinate<C>
    {
        protected NodeDataLayer(Topology<C> topology)
        {
            Topology = topology;
        }

        public Topology<C> Topology { get; private set; }

        #region IMapping<C,B> Members

        public abstract B this[C key] { get; }

        #endregion
    }

    [ContractClassFor(typeof(NodeDataLayer<,>))]
    internal abstract class ContractNodeDataLayer<C, B>: NodeDataLayer<C, B>
        where C: ICoordinate<C>
    {
        protected ContractNodeDataLayer(Topology<C> topology): base(topology) {}

        #region Overrides of NodeDataLayer<C,B>

        public override B this[C key]
        {
            get
            {
                Contract.Requires(Topology.Lays(key));

                throw new NotImplementedException();
            }
        }

        #endregion
    }
}