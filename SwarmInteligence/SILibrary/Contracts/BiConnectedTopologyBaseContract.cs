using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using Common.Collections;
using SILibrary.General;

namespace SILibrary.Contracts
{
	[ContractClassFor(typeof(BiConnectedTopologyBase<>))]
	public abstract class BiConnectedTopologyBaseContract<TCoordinate>: BiConnectedTopologyBase<TCoordinate>
	{
		protected override IEnumerable<TCoordinate> GetNeighbours(TCoordinate coord)
		{
			Contract.Ensures(!Contract.Result<IEnumerable<TCoordinate>>().IsEmpty());
			throw new UreachableCodeException();
		}
	}
}