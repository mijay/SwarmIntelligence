using System.Collections.Generic;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General
{
	public abstract class BiConnectedTopologyBase<TCoordinate>: Topology<TCoordinate>
	{
		public override IEnumerable<TCoordinate> GetSuccessors(TCoordinate coord)
		{
			return GetNeighbours(coord);
		}

		public override IEnumerable<TCoordinate> GetPredecessors(TCoordinate coord)
		{
			return GetNeighbours(coord);
		}

		public override IEnumerable<TCoordinate> GetAdjacent(TCoordinate coord)
		{
			return GetNeighbours(coord);
		}

		protected abstract IEnumerable<TCoordinate> GetNeighbours(TCoordinate coord);
	}
}