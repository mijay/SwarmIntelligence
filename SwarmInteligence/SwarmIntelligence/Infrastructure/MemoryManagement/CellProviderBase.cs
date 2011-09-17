using System;
using System.Diagnostics.Contracts;
using System.Threading;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class CellProviderBase<TCoordinate, TNodeData, TEdgeData>: ICellProvider<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private MapBase<TCoordinate, TNodeData, TEdgeData> context;

		protected MapBase<TCoordinate, TNodeData, TEdgeData> Context
		{
			get { return context; }
		}

		protected abstract CellBase<TCoordinate, TNodeData, TEdgeData> ReuseOrBuild();
		protected abstract void ReturnForReuse(CellBase<TCoordinate, TNodeData, TEdgeData> cellBase);

		#region Implementation of ICellProvider<TCoordinate,TNodeData,TEdgeData>

		public void UseContext(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase)
		{
			Contract.Requires(mapBase != null);

			Interlocked.CompareExchange(ref context, mapBase, null);
			if(context != mapBase)
				throw new InvalidOperationException("CellProvider was already been initialized");
		}

		public void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell)
		{
			//todo: сделать уже свой хэлпер для таких случаев + пусть он и проверку контрактов делает
#if DEBUG
			if(context == null)
				throw new InvalidOperationException("Call UseContext first");
			if(cell.MapBase == context)
				throw new ArgumentException("Cell is from invalid map!");
#endif
			ReturnForReuse(cell);
		}

		public CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate)
		{
#if DEBUG
			if(context == null)
				throw new InvalidOperationException("Call UseContext first");
#endif
			CellBase<TCoordinate, TNodeData, TEdgeData> result = ReuseOrBuild();
			result.SetCoordinate(coordinate);
			return result;
		}

		#endregion
	}
}