using System.Collections.Generic;

namespace SwarmIntelligence.Core.Playground
{
	public interface ICell<TCoordinate, TNodeData, TEdgeData>: IEnumerable<IAnt<TCoordinate, TNodeData, TEdgeData>>
	{
		Map<TCoordinate, TNodeData, TEdgeData> Map { get; }
		TCoordinate Coordinate { get; }
	}
}