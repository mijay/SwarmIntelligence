using System.Collections.Generic;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	/// <summary>
	/// Class that represents one point of space as a placeholder for <see cref="IAnt{TCoordinate,TNodeData,TEdgeData}"/>s.
	/// </summary>
	/// <typeparam name="TCoordinate">Type of coordinates in represented space.</typeparam>
	/// <typeparam name="TNodeData">Type of data associated with points in current space.</typeparam>
	/// <typeparam name="TEdgeData">Type of data associated with edges between point in current space.</typeparam>
	public interface ICell<TCoordinate, TNodeData, TEdgeData>: IEnumerable<IAnt<TCoordinate, TNodeData, TEdgeData>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
	}
}