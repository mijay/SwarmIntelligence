using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SILibrary.Contracts;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General
{
	[ContractClass(typeof(BiConnectedTopologyBaseContract<>))]
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