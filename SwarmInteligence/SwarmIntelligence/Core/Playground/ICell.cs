using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SwarmIntelligence.Core.Playground
{
	public interface ICell<TCoordinate, TNodeData, TEdgeData>: IEnumerable<IAnt<TCoordinate, TNodeData, TEdgeData>>
	{
		[Pure]
		IMap<TCoordinate, TNodeData, TEdgeData> Map { get; }

		[Pure]
		TCoordinate Coordinate { get; }
	}
}