using System;
using System.Threading;
using Common;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class CellProviderBase<TCoordinate, TNodeData, TEdgeData>: ICellProvider<TCoordinate, TNodeData, TEdgeData>
	{
		private MapBase<TCoordinate, TNodeData, TEdgeData> context;

		protected MapBase<TCoordinate, TNodeData, TEdgeData> Context
		{
			get { return context; }
		}

		protected abstract CellBase<TCoordinate, TNodeData, TEdgeData> ReuseOrBuild();
		protected abstract void ReturnForReuse(CellBase<TCoordinate, TNodeData, TEdgeData> cellBase);

		#region Implementation of ICellProvider<TCoordinate,TNodeData,TEdgeData>

		public void SetContext(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase)
		{
			Requires.NotNull(mapBase);

			Interlocked.CompareExchange(ref context, mapBase, null);
			if(context != mapBase)
				throw new InvalidOperationException("CellProvider was already been initialized");
		}

		public void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Requires.NotNull<InvalidOperationException>(context);
			Requires.True(cell.MapBase == context);

			ReturnForReuse(cell);
		}

		public CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate)
		{
			Requires.NotNull<InvalidOperationException>(context);

			CellBase<TCoordinate, TNodeData, TEdgeData> result = ReuseOrBuild();
			result.SetCoordinate(coordinate);
			return result;
		}

		#endregion
	}
}