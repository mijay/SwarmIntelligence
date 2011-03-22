using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.World.Space;

namespace SwarmIntelligence2.Core.World.Data
{
    [ContractClass(typeof(ContractBackground<,>))]
    public abstract class Background<C, B>: IMapping<C, B>
        where C: ICoordinate<C>
    {
        protected Background(Boundaries<C> boundaries)
        {
            Boundaries = boundaries;
        }

        public Boundaries<C> Boundaries { get; private set; }

        #region IMapping<C,B> Members

        public abstract B this[C key] { get; }

        #endregion
    }

    [ContractClassFor(typeof(Background<,>))]
    internal abstract class ContractBackground<C,B>: Background<C,B>
        where C: ICoordinate<C>
    {
        protected ContractBackground(Boundaries<C> boundaries): base(boundaries) {}

        #region Overrides of Background<C,B>

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