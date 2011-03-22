﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SwarmIntelligence2.Core.World.Space
{
    [ContractClass(typeof(ContractTopology<>))]
    public abstract class Topology<C>
        where C: ICoordinate<C>
    {
        protected Topology(Boundaries<C> boundaries)
        {
            Boundaries = boundaries;
        }

        public Boundaries<C> Boundaries { get; private set; }

        public abstract IEnumerable<C> GetSuccessors(C coord);
        public abstract IEnumerable<C> GetPredecessors(C coord);

        public virtual bool Exists(Edge<C> edge)
        {
            Contract.Requires(Boundaries.Lays(edge));

            return GetSuccessors(edge.from).Contains(edge.to);
        }

        public IEnumerable<Edge<C>> GetOutgoing(C coord)
        {
            return GetSuccessors(coord).Select(x => new Edge<C>(coord, x));
        }

        public IEnumerable<Edge<C>> GetIncoming(C coord)
        {
            return GetPredecessors(coord).Select(x => new Edge<C>(x, coord));
        }

        public IEnumerable<Edge<C>> GetAdjacent(C coord)
        {
            return GetOutgoing(coord).Concat(GetIncoming(coord));
        }
    }

    [ContractClassFor(typeof(Topology<>))]
    internal abstract class ContractTopology<C>: Topology<C>
        where C: ICoordinate<C>
    {
        protected ContractTopology(Boundaries<C> boundaries): base(boundaries) {}

        #region Overrides of Topology<C>

        public override IEnumerable<C> GetSuccessors(C coord)
        {
            Contract.Requires(Boundaries.Lays(coord));
            throw new NotImplementedException();
        }

        public override IEnumerable<C> GetPredecessors(C coord)
        {
            Contract.Requires(Boundaries.Lays(coord));
            throw new NotImplementedException();
        }

        #endregion
    }
}