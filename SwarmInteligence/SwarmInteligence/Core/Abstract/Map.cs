using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    public class Map<C, B>
        where C: struct, ICoordinate<C>
    {
        private readonly Tuple<C, C> bounds;

        public Map(Tuple<C, C> bounds, IGauge<C> gauge)
        {
            Contract.Requires<ArgumentNullException>(bounds != null && gauge != null);
            this.bounds = bounds;
            this.gauge = gauge;
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
            Contract.Invariant(bounds != null && gauge != null);
        }

        protected readonly IGauge<C> gauge;

        [Pure]
        public IGauge<C> Gauge
        {
            get
            {
                Contract.Ensures(Contract.Result<IGauge<C>>() != null);
                return gauge;
            }
        }
    }
}