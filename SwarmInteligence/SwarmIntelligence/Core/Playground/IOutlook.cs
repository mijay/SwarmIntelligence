using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	public interface IOutlook<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		[Pure]
		World<TCoordinate, TNodeData, TEdgeData> World { get; }

		[Pure]
		IMap<TCoordinate, TNodeData, TEdgeData> Map { get; }

		[Pure]
		DataLayer<TCoordinate, TNodeData> NodesData { get; }

		[Pure]
		DataLayer<Edge<TCoordinate>, TEdgeData> EdgesData { get; }

		[Pure]
		ICell<TCoordinate, TNodeData, TEdgeData> Cell { get; }

		[Pure]
		IAnt<TCoordinate, TNodeData, TEdgeData> Me { get; }

		[Pure]
		TCoordinate Coordinate { get; }

		[Pure]
		ILog Log { get; }
	}
}