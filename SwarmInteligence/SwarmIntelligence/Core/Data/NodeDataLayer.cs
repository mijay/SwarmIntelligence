using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Data
{
    [ContractClass(typeof(ContractNodeDataLayer<,>))]
    public abstract class NodeDataLayer<C, B>: IMapping<C, B>
        where C: ICoordinate<C>
    {
        protected NodeDataLayer(Boundaries<C> boundaries)
        {
            Boundaries = boundaries;
        }

        public Boundaries<C> Boundaries { get; private set; }

        #region IMapping<C,B> Members

        public abstract B this[C key] { get; }

        #endregion
    }

    [ContractClassFor(typeof(NodeDataLayer<,>))]
    internal abstract class ContractNodeDataLayer<C, B>: NodeDataLayer<C, B>
        where C: ICoordinate<C>
    {
        protected ContractNodeDataLayer(Boundaries<C> boundaries): base(boundaries) {}

        #region Overrides of NodeDataLayer<C,B>

        public override B this[C key]
        {
            get
            {
                Contract.Requires(Boundaries.Lays(key));

                throw new NotImplementedException();
            }
        }

        #endregion
    }
}