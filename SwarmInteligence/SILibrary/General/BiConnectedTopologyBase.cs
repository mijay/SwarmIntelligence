using System.Collections.Generic;
using Common;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General
{
	public abstract class BiConnectedTopologyBase<TCoordinate>: Topology<TCoordinate>
	{
		public override IEnumerable<TCoordinate> GetSuccessors(TCoordinate coord)
		{
			Requires.True(Lays(coord)); //todo: вернуть контракты!!!!
			return GetNeighbours(coord);
		}

		public override IEnumerable<TCoordinate> GetPredecessors(TCoordinate coord)
		{
			Requires.True(Lays(coord));
			return GetNeighbours(coord);
		}

		public override IEnumerable<TCoordinate> GetAdjacent(TCoordinate coord)
		{
			Requires.True(Lays(coord));
			return GetNeighbours(coord);
		}

		protected abstract IEnumerable<TCoordinate> GetNeighbours(TCoordinate coord);
	}
}