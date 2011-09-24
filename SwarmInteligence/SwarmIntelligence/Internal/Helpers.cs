using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
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
			Contract.Requires(cell is CellBase<TCoordinate, TNodeData, TEdgeData>);
			return (CellBase<TCoordinate, TNodeData, TEdgeData>) cell;
		}

		public static AntBase<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this IAnt<TCoordinate, TNodeData, TEdgeData> ant)
		{
			Contract.Requires(ant is AntBase<TCoordinate, TNodeData, TEdgeData>);
			return (AntBase<TCoordinate, TNodeData, TEdgeData>) ant;
		}

		public static MapBase<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this IMap<TCoordinate, TNodeData, TEdgeData> map)
		{
			Contract.Requires(map is MapBase<TCoordinate, TNodeData, TEdgeData>);
			return (MapBase<TCoordinate, TNodeData, TEdgeData>) map;
		}
	}
}