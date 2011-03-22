using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.Data;
using SwarmIntelligence2.Core.Space;

namespace SwarmIntelligence2.Core.Creatures
{
    [ContractClass(typeof(ContractMap<,,>))]
    public abstract class Map<C, B, E>: ILazyMapping<C, Cell<C, B, E>>
        where C: ICoordinate<C>
    {
        protected Map(Boundaries<C> boundaries)
        {
            Boundaries = boundaries;
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

        public Boundaries<C> Boundaries { get; set; }
    }

    [ContractClassFor(typeof(Map<,,>))]
    internal abstract class ContractMap<C, B, E>: Map<C, B, E>
        where C: ICoordinate<C>
    {
        protected ContractMap(Boundaries<C> boundaries): base(boundaries) {}

        public override Cell<C, B, E> this[C key]
        {
            get
            {
                Contract.Requires(Boundaries.Lays(key));

                throw new NotImplementedException();
            }
        }

        public override bool IsInitialized(C key)
        {
            Contract.Requires(Boundaries.Lays(key));

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