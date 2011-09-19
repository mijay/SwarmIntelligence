using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	public interface IOutlook<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		World<TCoordinate, TNodeData, TEdgeData> World { get; }
		Map<TCoordinate, TNodeData, TEdgeData> Map { get; }
		DataLayer<TCoordinate, TNodeData> NodesData { get; }
		DataLayer<Edge<TCoordinate>, TEdgeData> EdgesData { get; }

		Cell<TCoordinate, TNodeData, TEdgeData> Cell { get; }
		Ant<TCoordinate, TNodeData, TEdgeData> Me { get; }
		TCoordinate Coordinate { get; }
	}
}