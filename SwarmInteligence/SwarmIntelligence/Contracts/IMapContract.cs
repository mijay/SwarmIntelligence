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
	{
		#region Implementation of IMapping<in TCoordinate,out ICell<TCoordinate,TNodeData,TEdgeData>>

		public ICell<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate)
		{
			Contract.Requires(Topology.Lays(coordinate));
			Contract.Ensures(Contract.Result<ICell<TCoordinate, TNodeData, TEdgeData>>() != null);
			Contract.Ensures(Contract.Result<ICell<TCoordinate, TNodeData, TEdgeData>>().Coordinate.Equals(coordinate));
			Contract.Ensures(Contract.Result<ICell<TCoordinate, TNodeData, TEdgeData>>().Map == this);
			throw new UreachableCodeException();
		}

		#endregion

		#region Implementation of ISparseMappint<in TCoordinate,ICell<TCoordinate,TNodeData,TEdgeData>>

		public bool Has(TCoordinate key)
		{
			Contract.Requires(Topology.Lays(key));
			throw new UreachableCodeException();
		}

		public bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Contract.Requires(Topology.Lays(coordinate));
			Contract.Ensures(Contract.Result<bool>() ? Contract.ValueAtReturn(out cell) != null : true);
			Contract.Ensures(Contract.Result<bool>() ? Contract.ValueAtReturn(out cell).Coordinate.Equals(coordinate) : true);
			Contract.Ensures(Contract.Result<bool>() ? Contract.ValueAtReturn(out cell).Map == this : true);
			throw new UreachableCodeException();
		}

		#endregion

		#region Implementation of IEnumerable

		public IEnumerator<KeyValuePair<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>> GetEnumerator()
		{
			throw new UreachableCodeException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Implementation of IMap<TCoordinate,TNodeData,TEdgeData>

		public Topology<TCoordinate> Topology
		{
			get { throw new UreachableCodeException(); }
		}

		#endregion
	}
}