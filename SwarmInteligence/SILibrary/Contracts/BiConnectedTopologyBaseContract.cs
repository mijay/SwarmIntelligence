using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using Common.Collections.Extensions;
using SILibrary.General;
using SwarmIntelligence.Core.Space;

namespace SILibrary.Contracts
{
	[ContractClassFor(typeof(BiConnectedTopologyBase<>))]
	public abstract class BiConnectedTopologyBaseContract<TCoordinate>: BiConnectedTopologyBase<TCoordinate>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		protected override IEnumerable<TCoordinate> GetNeighbours(TCoordinate coord)
		{
			Contract.Ensures(!Contract.Result<IEnumerable<TCoordinate>>().IsEmpty());
			throw new UreachableCodeException();
		}
	}
}