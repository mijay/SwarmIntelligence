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
		/// <summary>
		/// <see cref="IMap{TCoordinate,TNodeData,TEdgeData}"/> to which current <see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/> belongs.
		/// </summary>
		IMap<TCoordinate, TNodeData, TEdgeData> Map { get; }

		/// <summary>
		/// <typeparamref name="TCoordinate"/> of the point represented by current <see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/>
		/// </summary>
		TCoordinate Coordinate { get; }
	}
}