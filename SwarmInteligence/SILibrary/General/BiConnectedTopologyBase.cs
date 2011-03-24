using System.Collections.Generic;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General
{
    public abstract class BiConnectedTopologyBase<C>: Topology<C>
        where C: ICoordinate<C>
    {
        protected BiConnectedTopologyBase(Boundaries<C> boundaries): base(boundaries) {}

        public override IEnumerable<C> GetSuccessors(C coord)
        {
            return GetNeighbours(coord);
        }

        public override IEnumerable<C> GetPredecessors(C coord)
        {
            return GetNeighbours(coord);
        }

        protected abstract IEnumerable<C> GetNeighbours(C coord);
    }
}