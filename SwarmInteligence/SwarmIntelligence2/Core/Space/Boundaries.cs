using System.Diagnostics.Contracts;

namespace SwarmIntelligence2.Core.Space
{
    public abstract class Boundaries<C>
        where C: ICoordinate<C>
    {
        [Pure]
        public abstract bool Lays(C coord);

        [Pure]
        public bool Lays(Edge<C> edge)
        {
            return Lays(edge.from) && Lays(edge.to);
        }
    }
}