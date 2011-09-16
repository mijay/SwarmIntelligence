using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.General.Playground
{
	public class CellProvider<TCoordinate, TNodeData, TEdgeData>: CellProviderBase<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly ConcurrentBag<CellBase<TCoordinate, TNodeData, TEdgeData>> bag =
			new ConcurrentBag<CellBase<TCoordinate, TNodeData, TEdgeData>>();

		private readonly Func<CellBase<TCoordinate, TNodeData, TEdgeData>> cellBuilder;

		public CellProvider(Func<CellBase<TCoordinate, TNodeData, TEdgeData>> cellBuilder)
		{
			Contract.Requires(cellBuilder != null && cellBuilder() != null);
			this.cellBuilder = cellBuilder;
		}

		#region Overrides of CellProviderBase<TCoordinate,TNodeData,TEdgeData>

		protected override CellBase<TCoordinate, TNodeData, TEdgeData> ReuseOrBuild()
		{
			CellBase<TCoordinate, TNodeData, TEdgeData> result;
			return bag.TryTake(out result) ? result : cellBuilder();
		}

		public override void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell)
		{
			bag.Add(cell);
		}

		#endregion
	}
}