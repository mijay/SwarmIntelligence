using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    public abstract class District<C, B>
        where C: struct, ICoordinate<C>
    {
        [Pure]
        public Map<C, B> Map { private set; get; }

        [Pure]
        public Air Air { private set; get; }

        [Pure]
        public TurnStage Stage { private set; get; }

        [Pure]
        public Tuple<C, C> Bounds { private set; get; }
    }
}