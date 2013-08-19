using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	/// <summary>
	/// Class that represents the active entity in the space - ant.
	/// </summary>
	/// <typeparam name="TCoordinate">Type of coordinates in represented space.</typeparam>
	/// <typeparam name="TNodeData">Type of data associated with points in current space.</typeparam>
	/// <typeparam name="TEdgeData">Type of data associated with edges between point in current space.</typeparam>
	public interface IAnt<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		/// <summary>
		/// Current coordinate of the <see cref="IAnt{TCoordinate,TNodeData,TEdgeData}"/>.
		/// </summary>
		[Pure]
		TCoordinate Coordinate { get; }

		/// <summary>
		/// <see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/> where the <see cref="IAnt{TCoordinate,TNodeData,TEdgeData}"/> is now located.
		/// </summary>
		[Pure]
		ICell<TCoordinate, TNodeData, TEdgeData> Cell { get; }

		/// <summary>
		/// Method which is invoked when the <see cref="IAnt{TCoordinate,TNodeData,TEdgeData}"/>'s work should be done.
		/// </summary>
		void ProcessTurn();
	}
}