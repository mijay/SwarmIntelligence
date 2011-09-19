using System;
using System.Collections.Concurrent;
using Common;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.General.Playground
{
	public class CellProvider<TCoordinate, TNodeData, TEdgeData>: CellProviderBase<TCoordinate, TNodeData, TEdgeData>
	{
		private readonly ConcurrentBag<CellBase<TCoordinate, TNodeData, TEdgeData>> bag =
			new ConcurrentBag<CellBase<TCoordinate, TNodeData, TEdgeData>>();

		private readonly Func<MapBase<TCoordinate, TNodeData, TEdgeData>, CellBase<TCoordinate, TNodeData, TEdgeData>> cellBuilder;

		public CellProvider(Func<MapBase<TCoordinate, TNodeData, TEdgeData>, CellBase<TCoordinate, TNodeData, TEdgeData>> cellBuilder)
		{
			Requires.NotNull(cellBuilder);
			this.cellBuilder = cellBuilder;
		}

		#region Overrides of CellProviderBase<TCoordinate,TNodeData,TEdgeData>

		protected override CellBase<TCoordinate, TNodeData, TEdgeData> ReuseOrBuild()
		{
			CellBase<TCoordinate, TNodeData, TEdgeData> result;
			return bag.TryTake(out result) ? result : cellBuilder(Context);
		}

		protected override void ReturnForReuse(CellBase<TCoordinate, TNodeData, TEdgeData> cellBase)
		{
			bag.Add(cellBase);
		}

		#endregion
	}
}