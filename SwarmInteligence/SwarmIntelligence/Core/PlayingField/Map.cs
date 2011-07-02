using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.PlayingField
{
    [ContractClass(typeof(ContractMap<,,>))]
    public abstract class Map<C, B, E>: ILazyMapping<C, Cell<C, B, E>>
        where C: ICoordinate<C>
    {
        protected Map(Topology<C> topology)
        {
            Topology = topology;
        }

        #region Implementation of IMapping<in C,out Cell<C,B,E>>

        public abstract Cell<C, B, E> this[C key] { get; }

        #endregion

        #region Implementation of ILazyMapping<C,Cell<C,B,E>>

        [Pure]
        public abstract bool IsInitialized(C key);

        public abstract IEnumerable<KeyValuePair<C, Cell<C, B, E>>> GetInitialized();
        public abstract void Free(C key);

        #endregion

        public Topology<C> Topology { get; set; }
    }

    [ContractClassFor(typeof(Map<,,>))]
    internal abstract class ContractMap<C, B, E>: Map<C, B, E>
        where C: ICoordinate<C>
    {
        protected ContractMap(Topology<C> topology): base(topology) {}

        public override Cell<C, B, E> this[C key]
        {
            get
            {
                Contract.Requires(Topology.Lays(key));

                throw new NotImplementedException();
            }
        }

        public override bool IsInitialized(C key)
        {
            Contract.Requires(Topology.Lays(key));

            throw new NotImplementedException();
        }

        public override void Free(C key)
        {
            Contract.Requires(IsInitialized(key));

            throw new NotImplementedException();
        }

        public override IEnumerable<KeyValuePair<C, Cell<C, B, E>>> GetInitialized()
        {
            Contract.Ensures(
                Contract.ForAll(Contract.Result<IEnumerable<KeyValuePair<C, Cell<C, B, E>>>>(),
                                pair => IsInitialized(pair.Key) && pair.Value == this[pair.Key]));

            throw new NotImplementedException();
        }
    }
}