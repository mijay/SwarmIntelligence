using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IMap<,,>))]
	public abstract class IMapContract<TCoordinate, TNodeData, TEdgeData>: IMap<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region IMap<TCoordinate,TNodeData,TEdgeData> Members

		public bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Contract.Requires(Topology.Lays(coordinate));
			Contract.Ensures(Contract.Result<bool>() ? Contract.ValueAtReturn(out cell) != null : true);
			Contract.Ensures(Contract.Result<bool>() ? Contract.ValueAtReturn(out cell).Coordinate.Equals(coordinate) : true);
			Contract.Ensures(Contract.Result<bool>() ? Contract.ValueAtReturn(out cell).Map == this : true);
			throw new UreachableCodeException();
		}

		public Topology<TCoordinate> Topology
		{
			get { throw new UreachableCodeException(); }
		}

		public IEnumerator<ICell<TCoordinate, TNodeData, TEdgeData>> GetEnumerator()
		{
			throw new UreachableCodeException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}