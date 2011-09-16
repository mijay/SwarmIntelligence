using System.Collections.Generic;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General
{
	public abstract class BiConnectedTopologyBase<C>: Topology<C>
		where C: ICoordinate<C>
	{
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