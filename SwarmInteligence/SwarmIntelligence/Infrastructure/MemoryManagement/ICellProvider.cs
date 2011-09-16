using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public interface ICellProvider<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell);
		CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate);
	}
}