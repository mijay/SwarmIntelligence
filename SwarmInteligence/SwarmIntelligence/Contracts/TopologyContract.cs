using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(Topology<>))]
	public abstract class TopologyContract<TCoordinate>: Topology<TCoordinate>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region Overrides of Topology<TCoordinate>

		public override bool Lays(TCoordinate coord)
		{
			throw new UnreachableCodeException();
		}

		public override IEnumerable<TCoordinate> GetSuccessors(TCoordinate coord)
		{
			Contract.Requires(Lays(coord));
			throw new UnreachableCodeException();
		}

		public override IEnumerable<TCoordinate> GetPredecessors(TCoordinate coord)
		{
			Contract.Requires(Lays(coord));
			throw new UnreachableCodeException();
		}

		#endregion
	}
}