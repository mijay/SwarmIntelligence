using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    [ContractClass(typeof(DistrictContract<,>))]
    public abstract class District<C, B>
        where C: struct, ICoordinate<C>
    {
        [Pure]
        protected District(Map<C, B> map, Air<C, B> air, Tuple<C, C> bounds, Background<C, B> background, IGauge<C> gauge)
        {
            Contract.Requires<ArgumentNullException>(map != null && air != null && bounds != null && background != null
                                                     && gauge != null);
            this.map = map;
            this.background = background;
            this.air = air;
            air.District = this;
            this.bounds = bounds;
            this.gauge = gauge;
        }

        [ContractInvariantMethod]
        private void DistrictInvariant()
        {
            Contract.Invariant(bounds != null && air != null && background != null && map != null && gauge != null);
        }

        [Pure]
        public abstract IList<Stone<C, B>> GetData(ICoordinate<C> coordinate);

        #region Fields

        protected readonly Air<C, B> air;
        protected readonly Background<C, B> background;
        protected readonly Tuple<C, C> bounds;
        protected readonly IGauge<C> gauge;
        protected readonly Map<C, B> map;

        [Pure]
        public IGauge<C> Gauge
        {
            get
            {
                Contract.Ensures(Contract.Result<IGauge<C>>() != null);
                return gauge;
            }
        }

        [Pure]
        public Map<C, B> Map
        {
            get
            {
                Contract.Ensures(Contract.Result<Map<C, B>>() != null);
                return map;
            }
        }

        [Pure]
        public Air<C, B> Air
        {
            get
            {
                Contract.Ensures(Contract.Result<Air<C, B>>() != null);
                return air;
            }
        }

        [Pure]
        public TurnStage Stage { protected set; get; }

        [Pure]
        public Tuple<C, C> Bounds
        {
            get
            {
                Contract.Ensures(Contract.Result<Tuple<C, C>>() != null);
                return bounds;
            }
        }

        [Pure]
        public Background<C, B> Background
        {
            get
            {
                Contract.Ensures(Contract.Result<Background<C, B>>() != null);
                return background;
            }
        }

        #endregion
    }

    [ContractClassFor(typeof(District<,>))]
    internal sealed class DistrictContract<C, B>: District<C, B>
        where C: struct, ICoordinate<C>
    {
        public DistrictContract(Map<C, B> map, Air<C, B> air, Tuple<C, C> bounds, Background<C, B> background, IGauge<C> gauge)
            : base(map, air, bounds, background, gauge) {}

        #region Overrides of District<C,B>

        public override IList<Stone<C, B>> GetData(ICoordinate<C> coordinate)
        {
            Contract.Requires<IndexOutOfRangeException>(coordinate.IsInRange(Bounds.Item1, Bounds.Item2));
            Contract.Ensures(Contract.Result<IList<Stone<C, B>>>() != null);
            throw new NotImplementedException();
        }

        #endregion
    }
}