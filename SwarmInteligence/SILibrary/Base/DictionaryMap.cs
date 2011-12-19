using System.Collections.Concurrent;
using System.Collections.Generic;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.Base
{
	public class DictionaryMap<TCoordinate, TNodeData, TEdgeData>: MapBase<TCoordinate, TNodeData, TEdgeData>
	{
		public readonly ConcurrentDictionary<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>> dictionary =
			new ConcurrentDictionary<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>();

		public DictionaryMap(Topology<TCoordinate> topology, ICellProvider<TCoordinate, TNodeData, TEdgeData> cellProvider, ILog log)
			: base(topology, cellProvider, log)
		{
		}

		#region Overrides of Map<TCoordinate,TNodeData,TEdgeData>

		public override bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell)
		{
			return dictionary.TryGetValue(coordinate, out cell);
		}

		public override IEnumerator<ICell<TCoordinate, TNodeData, TEdgeData>> GetEnumerator()
		{
			return dictionary.Values.GetEnumerator();
		}

		#endregion

		#region Overrides of MapBase<TCoordinate,TNodeData,TEdgeData>

		protected override void Remove(TCoordinate coordinate)
		{
			ICell<TCoordinate, TNodeData, TEdgeData> _;
			dictionary.TryRemove(coordinate, out _);
		}

		protected override ICell<TCoordinate, TNodeData, TEdgeData> GetOrAdd(TCoordinate coordinate,
		                                                                     ICell<TCoordinate, TNodeData, TEdgeData> cell)
		{
			return dictionary.GetOrAdd(coordinate, cell);
		}

		#endregion
	}
}