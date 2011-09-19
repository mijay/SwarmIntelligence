using System.Collections.Generic;
using Common.Concurrent;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.Common
{
	public class SetCell<TCoordinate, TNodeData, TEdgeData>: CellBase<TCoordinate, TNodeData, TEdgeData>
	{
		private readonly ConcurrentSet<IAnt<TCoordinate, TNodeData, TEdgeData>> set =
			new ConcurrentSet<IAnt<TCoordinate, TNodeData, TEdgeData>>();

		public SetCell(MapBase<TCoordinate, TNodeData, TEdgeData> map)
			: base(map)
		{
		}

		#region Overrides of Cell<TCoordinate,TNodeData,TEdgeData>

		public override IEnumerator<IAnt<TCoordinate, TNodeData, TEdgeData>> GetEnumerator()
		{
			return set.GetEnumerator();
		}

		#endregion

		#region Overrides of CellBase<TCoordinate,TNodeData,TEdgeData>

		public override bool IsEmpty
		{
			get { return set.IsEmpty; }
		}

		public override void Add(IAnt<TCoordinate, TNodeData, TEdgeData> ant)
		{
			set.Add(ant);
		}

		public override void Remove(IAnt<TCoordinate, TNodeData, TEdgeData> ant)
		{
			set.Remove(ant);
		}

		#endregion

		public static CellProvider<TCoordinate, TNodeData, TEdgeData> Provider()
		{
			return new CellProvider<TCoordinate, TNodeData, TEdgeData>(map => new SetCell<TCoordinate, TNodeData, TEdgeData>(map));
		}
	}
}