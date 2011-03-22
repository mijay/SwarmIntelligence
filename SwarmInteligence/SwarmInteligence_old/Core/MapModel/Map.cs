using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    public abstract class Map<C, B>
        where C: struct, ICoordinate<C>
    {
        protected Map(IGauge<C> gauge, Air<C, B> air, Background<C, B> background, District<C, B> district)
        {
            Contract.Requires<ArgumentNullException>(gauge != null && air != null && background != null && district != null);
            Gauge = gauge;
            Air = air;
            Background = background;
            District = district;
        }

        [Pure]
        public IGauge<C> Gauge { get; private set; }

        [Pure]
        public Air<C, B> Air { get; private set; }

        [Pure]
        public TurnStage Stage { get; protected set; }

        [Pure]
        public Background<C, B> Background { get; private set; }

        [Pure]
        public District<C, B> District { get; set; }


        #region Events

        public abstract event Action OnBeforeTurnStage;
        public abstract event Action OnTurnStage;
        public abstract event Action OnApplyTurnStage;
        public abstract event Action OnAfterTurnStage;
        public abstract event Action OnApplyAfterTurnStage;

        #endregion
    }
}