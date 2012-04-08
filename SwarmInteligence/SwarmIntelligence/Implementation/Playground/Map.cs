using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.MemoryManagement;

namespace SwarmIntelligence.Implementation.Playground
{
	public class Map<TCoordinate, TNodeData, TEdgeData>: IMap<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly IValueProvider<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> valueProvider;
		private readonly IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> valueStorage;

		public Map(Topology<TCoordinate> topology, ValueProviderBuilder<TCoordinate, TNodeData, TEdgeData> valueProviderBuilder,
		           IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> valueStorage)
		{
			Contract.Requires(topology != null && valueProviderBuilder != null && valueStorage != null);
			Topology = topology;
			this.valueStorage = valueStorage;
			valueProvider = valueProviderBuilder(this);
		}

		#region IMap<TCoordinate,TNodeData,TEdgeData> Members

		public Topology<TCoordinate> Topology { get; private set; }

		public bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(coordinate));
			Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out cell).Coordinate.Equals(coordinate));

			CellBase<TCoordinate, TNodeData, TEdgeData> cellBase;
			bool result = valueStorage.TryGet(coordinate, out cellBase);
			cell = cellBase;
			return result;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<KeyValuePair<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>> GetEnumerator()
		{
			return valueStorage
				.Select(x => new KeyValuePair<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>(x.Key, x.Value))
				.GetEnumerator();
		}

		#endregion

		#region Internal interface

		internal CellBase<TCoordinate, TNodeData, TEdgeData> ForcedGet(TCoordinate coordinate)
		{
			Contract.Requires(Topology.Lays(coordinate));
			Contract.Ensures(Contract.Result<CellBase<TCoordinate, TNodeData, TEdgeData>>().Coordinate.Equals(coordinate));

			return valueStorage.GetOrCreate(coordinate, c => valueProvider.Get(c));
		}

		#endregion
	}
}