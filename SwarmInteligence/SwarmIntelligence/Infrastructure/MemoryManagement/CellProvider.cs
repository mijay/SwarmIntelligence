using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class CellProvider<TCoordinate, TNodeData, TEdgeData>: ICellProvider<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		protected abstract CellBase<TCoordinate, TNodeData, TEdgeData> ReuseOrBuild();

		#region Implementation of ICellProvider<TCoordinate,TNodeData,TEdgeData>

		public abstract void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell);

		public CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate)
		{
			CellBase<TCoordinate, TNodeData, TEdgeData> result = ReuseOrBuild();
			result.SetCoordinate(coordinate);
			return result;
		}

		#endregion
	}
}