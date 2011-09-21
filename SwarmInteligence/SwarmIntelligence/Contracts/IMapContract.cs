using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IMap<,,>))]
	public abstract class IMapContract<TCoordinate, TNodeData, TEdgeData> : IMap<TCoordinate, TNodeData, TEdgeData>
	{
		#region Implementation of IMapping<in TCoordinate,out ICell<TCoordinate,TNodeData,TEdgeData>>

		public ICell<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate key)
		{
			Contract.Requires(Topology.Lays(key));
			Contract.Ensures(Contract.Result<ICell<TCoordinate, TNodeData, TEdgeData>>() != null);
			Contract.Ensures(Contract.Result<ICell<TCoordinate, TNodeData, TEdgeData>>().Coordinate.Equals(key));
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

		public bool TryGet(TCoordinate key, out ICell<TCoordinate, TNodeData, TEdgeData> data)
		{
			Contract.Requires(Topology.Lays(key));
			Contract.Ensures(Contract.Result<bool>() ? Contract.ValueAtReturn(out data) != null : true);
			Contract.Ensures(Contract.Result<bool>() ? Contract.ValueAtReturn(out data).Coordinate.Equals(key) : true);
			Contract.Ensures(Contract.Result<bool>() ? Contract.ValueAtReturn(out data).Map == this : true);
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