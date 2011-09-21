using System.Diagnostics.Contracts;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public interface ICellProvider<TCoordinate, TNodeData, TEdgeData>
	{
		MapBase<TCoordinate, TNodeData, TEdgeData> Context { [Pure] get; set; }

		void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell);
		CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate);
	}
}