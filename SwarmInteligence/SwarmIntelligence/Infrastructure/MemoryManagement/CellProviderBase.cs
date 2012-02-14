using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class CellProviderBase<TCoordinate, TNodeData, TEdgeData>: ICellProvider<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region ICellProvider<TCoordinate,TNodeData,TEdgeData> Members

		public MapBase<TCoordinate, TNodeData, TEdgeData> Context { get; set; }

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