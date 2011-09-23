using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	[ContractClass(typeof(IMapContract<,,>))]
	public interface IMap<TCoordinate, TNodeData, TEdgeData>
		: IEnumerable<ICell<TCoordinate, TNodeData, TEdgeData>>
	{
		[Pure]
		Topology<TCoordinate> Topology { get; }

		/// <summary>
		/// Checks does the <see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/> exists in <see cref="IMap{TCoordinate,TNodeData,TEdgeData}"/>
		/// at <paramref name="coordinate"/>. And if true returns it.
		/// </summary>
		/// <param name="coordinate">Coordinate to check.</param>
		/// <param name="cell"><see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/> found in <see cref="IMap{TCoordinate,TNodeData,TEdgeData}"/> at given coordinate.</param>
		/// <returns>true if <paramref name="cell"/> was found. Otherwise - false.</returns>
		[Pure]
		bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell);
	}
}