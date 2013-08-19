using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SILibrary.Contracts;
using SwarmIntelligence.Core.Space;

namespace SILibrary.Common
{
	[ContractClass(typeof(BiConnectedTopologyBaseContract<>))]
	public abstract class BiConnectedTopologyBase<TCoordinate>: Topology<TCoordinate>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public override sealed IEnumerable<TCoordinate> GetSuccessors(TCoordinate coord)
		{
			return GetNeighbours(coord);
		}

		public override sealed IEnumerable<TCoordinate> GetPredecessors(TCoordinate coord)
		{
			return GetNeighbours(coord);
		}

		public override sealed IEnumerable<TCoordinate> GetAdjacent(TCoordinate coord)
		{
			return GetNeighbours(coord);
		}

		protected abstract IEnumerable<TCoordinate> GetNeighbours(TCoordinate coord);
	}
}