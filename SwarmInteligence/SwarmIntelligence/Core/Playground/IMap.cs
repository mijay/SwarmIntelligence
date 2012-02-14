using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	/// <summary>
	/// Class that represents the <see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/>s associated with points in current space.
	/// </summary>
	/// <typeparam name="TCoordinate">Type of coordinates in represented space.</typeparam>
	/// <typeparam name="TNodeData">Type of data associated with points in current space.</typeparam>
	/// <typeparam name="TEdgeData">Type of data associated with edges between point in current space.</typeparam>
	[ContractClass(typeof(IMapContract<,,>))]
	public interface IMap<TCoordinate, TNodeData, TEdgeData>: IEnumerable<ICell<TCoordinate, TNodeData, TEdgeData>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		/// <summary>
		/// <see cref="Topology{TCoordinate}"/> of the current sapce.
		/// </summary>
		[Pure]
		Topology<TCoordinate> Topology { get; }

		/// <summary>
		/// Checks does the <see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/> exists in <see cref="IMap{TCoordinate,TNodeData,TEdgeData}"/>
		/// at <paramref name="coordinate"/>. And if true returns it.
		/// </summary>
		/// <param name="coordinate">Coordinate to check.</param>
		/// <param name="cell"><see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/> found in <see cref="IMap{TCoordinate,TNodeData,TEdgeData}"/> at given coordinate.</param>
		/// <returns><c>true</c> if <paramref name="cell"/> was found. Otherwise - <c>false</c>.</returns>
		[Pure]
		bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell);
	}
}