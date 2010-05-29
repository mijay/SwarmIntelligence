using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    public class Map<C, B>
        where C: struct, ICoordinate<C>
    {
        private readonly Tuple<C, C> bounds;

        public Map(Tuple<C, C> bounds)
        {
            Contract.Requires<ArgumentNullException>(bounds != null);
            this.bounds = bounds;
        }

        [Pure]
        public Tuple<C, C> Bounds
        {
            get
            {
                Contract.Ensures(Contract.Result<Tuple<C, C>>() != null);
                return bounds;
            }
        }

        [ContractInvariantMethod]
        private void MapInvariant()
        {
            Contract.Invariant(bounds != null);
        }
    }
}