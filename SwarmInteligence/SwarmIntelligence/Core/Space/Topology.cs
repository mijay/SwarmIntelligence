using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SwarmIntelligence.Core.Space
{
    [ContractClass(typeof(ContractTopology<>))]
    public abstract class Topology<C>
        where C: ICoordinate<C>
    {
        [Pure]
        public abstract bool Lays(C coord);

        [Pure]
        public abstract IEnumerable<C> GetSuccessors(C coord);

        [Pure]
        public abstract IEnumerable<C> GetPredecessors(C coord);

        [Pure]
        public bool Lays(Edge<C> edge)
        {
            return Lays(edge.from) && Lays(edge.to);
        }

        [Pure]
        public virtual bool Exists(Edge<C> edge)
        {
            Contract.Requires(Lays(edge));
            return GetSuccessors(edge.from).Contains(edge.to);
        }
            
        [Pure]
        public IEnumerable<Edge<C>> GetOutgoing(C coord)
        {
            Contract.Requires(Lays(coord));
            return GetSuccessors(coord).Select(x => new Edge<C>(coord, x));
        }

        [Pure]
        public IEnumerable<Edge<C>> GetIncoming(C coord)
        {
            Contract.Requires(Lays(coord));
            return GetPredecessors(coord).Select(x => new Edge<C>(x, coord));
        }

        [Pure]
        public IEnumerable<Edge<C>> GetAdjacent(C coord)
        {
            Contract.Requires(Lays(coord));
            return GetOutgoing(coord).Concat(GetIncoming(coord));
        }
    }

    [ContractClassFor(typeof(Topology<>))]
    internal abstract class ContractTopology<C>: Topology<C>
        where C: ICoordinate<C>
    {
        #region Overrides of Topology<C>

        public override IEnumerable<C> GetSuccessors(C coord)
        {
            Contract.Requires(Lays(coord));
            throw new NotImplementedException();
        }

        public override IEnumerable<C> GetPredecessors(C coord)
        {
            Contract.Requires(Lays(coord));
            throw new NotImplementedException();
        }

        #endregion
    }
}