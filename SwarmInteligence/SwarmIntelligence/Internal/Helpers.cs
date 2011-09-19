using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace SwarmIntelligence.Internal
{
	internal static class Helpers
	{
		public static CellBase<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this ICell<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Requires.True(cell is CellBase<TCoordinate, TNodeData, TEdgeData>);
			return (CellBase<TCoordinate, TNodeData, TEdgeData>) cell;
		}

		public static AntBase<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this IAnt<TCoordinate, TNodeData, TEdgeData> ant)
		{
			Requires.True(ant is AntBase<TCoordinate, TNodeData, TEdgeData>);
			return (AntBase<TCoordinate, TNodeData, TEdgeData>) ant;
		}

		public static MapBase<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this Map<TCoordinate, TNodeData, TEdgeData> map)
		{
			Requires.True(map is MapBase<TCoordinate, TNodeData, TEdgeData>);
			return (MapBase<TCoordinate, TNodeData, TEdgeData>) map;
		}
	}
}