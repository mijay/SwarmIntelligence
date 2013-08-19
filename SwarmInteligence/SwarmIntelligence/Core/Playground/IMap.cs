using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	/// <summary>
	/// Class that represents the space as a placeholder for <see cref="IAnt{TCoordinate,TNodeData,TEdgeData}"/>s.
	/// </summary>
	/// <typeparam name="TCoordinate">Type of coordinates in represented space.</typeparam>
	/// <typeparam name="TNodeData">Type of data associated with points in current space.</typeparam>
	/// <typeparam name="TEdgeData">Type of data associated with edges between point in current space.</typeparam>
	[ContractClass(typeof(IMapContract<,,>))]
	public interface IMap<TCoordinate, TNodeData, TEdgeData>: ISparsedMapping<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		/// <summary>
		/// <see cref="Topology{TCoordinate}"/> of the current space.
		/// </summary>
		[Pure]
		Topology<TCoordinate> Topology { get; }
	}
}