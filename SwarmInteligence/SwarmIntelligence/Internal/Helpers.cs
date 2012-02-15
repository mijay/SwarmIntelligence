using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Playground;

namespace SwarmIntelligence.Internal
{
	internal static class Helpers
	{
		public static CellBase<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this ICell<TCoordinate, TNodeData, TEdgeData> cell)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			Contract.Requires(cell is CellBase<TCoordinate, TNodeData, TEdgeData>);
			return (CellBase<TCoordinate, TNodeData, TEdgeData>) cell;
		}

		public static AntBase<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this IAnt<TCoordinate, TNodeData, TEdgeData> ant)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			Contract.Requires(ant is AntBase<TCoordinate, TNodeData, TEdgeData>);
			return (AntBase<TCoordinate, TNodeData, TEdgeData>) ant;
		}

		public static Map<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this IMap<TCoordinate, TNodeData, TEdgeData> map)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			Contract.Requires(map is Map<TCoordinate, TNodeData, TEdgeData>);
			return (Map<TCoordinate, TNodeData, TEdgeData>) map;
		}
	}
}