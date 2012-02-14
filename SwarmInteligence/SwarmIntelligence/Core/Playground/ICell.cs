using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	public interface ICell<TCoordinate, TNodeData, TEdgeData>: IEnumerable<IAnt<TCoordinate, TNodeData, TEdgeData>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		[Pure]
		IMap<TCoordinate, TNodeData, TEdgeData> Map { get; }

		[Pure]
		TCoordinate Coordinate { get; }
	}
}