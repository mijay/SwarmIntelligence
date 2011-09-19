namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public interface ICellProvider<TCoordinate, TNodeData, TEdgeData>
	{
		void SetContext(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase);
		void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell);
		CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate);
	}
}