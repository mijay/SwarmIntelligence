using System;
using System.Threading;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class CellProviderBase<TCoordinate, TNodeData, TEdgeData>: ICellProvider<TCoordinate, TNodeData, TEdgeData>
	{
		private MapBase<TCoordinate, TNodeData, TEdgeData> context;

		#region ICellProvider<TCoordinate,TNodeData,TEdgeData> Members

		public MapBase<TCoordinate, TNodeData, TEdgeData> Context
		{
			get { return context; }
			set
			{
				Interlocked.CompareExchange(ref context, value, null);
				if(context != value)
					throw new InvalidOperationException("CellProvider was already been initialized");
			}
		}

		public abstract void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell);

		public CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate)
		{
			CellBase<TCoordinate, TNodeData, TEdgeData> result = ReuseOrBuild();
			result.SetCoordinate(coordinate);
			return result;
		}

		#endregion

		protected abstract CellBase<TCoordinate, TNodeData, TEdgeData> ReuseOrBuild();
	}
}