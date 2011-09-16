using System.Collections.Concurrent;
using System.Collections.Generic;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.General
{
	public class DictionaryMap<TCoordinate, TNodeData, TEdgeData>: MapBase<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public readonly ConcurrentDictionary<TCoordinate, Cell<TCoordinate, TNodeData, TEdgeData>> dictionary =
			new ConcurrentDictionary<TCoordinate, Cell<TCoordinate, TNodeData, TEdgeData>>();

		public DictionaryMap(Topology<TCoordinate> topology, ICellProvider<TCoordinate, TNodeData, TEdgeData> cellProvider)
			: base(topology, cellProvider) {}

		#region Overrides of Map<TCoordinate,TNodeData,TEdgeData>

		public override bool TryGet(TCoordinate key, out Cell<TCoordinate, TNodeData, TEdgeData> data)
		{
			return dictionary.TryGetValue(key, out data);
		}

		public override IEnumerator<KeyValuePair<TCoordinate, Cell<TCoordinate, TNodeData, TEdgeData>>> GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}

		#endregion

		#region Overrides of MapBase<TCoordinate,TNodeData,TEdgeData>

		protected override void Remove(TCoordinate coordinate)
		{
			Cell<TCoordinate, TNodeData, TEdgeData> _;
			dictionary.TryRemove(coordinate, out _);
		}

		protected override Cell<TCoordinate, TNodeData, TEdgeData> GetOrAdd(TCoordinate coordinate,
		                                                                    Cell<TCoordinate, TNodeData, TEdgeData> cell)
		{
			return dictionary.GetOrAdd(coordinate, cell);
		}

		#endregion
	}
}