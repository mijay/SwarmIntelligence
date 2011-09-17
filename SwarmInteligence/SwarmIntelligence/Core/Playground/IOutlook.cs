using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	public interface IOutlook<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		World<TCoordinate, TNodeData, TEdgeData> World { get; }
		TCoordinate Coordinate { get; }
		Cell<TCoordinate, TNodeData, TEdgeData> Cell { get; }
		Ant<TCoordinate, TNodeData, TEdgeData> Me { get; }
	}
}