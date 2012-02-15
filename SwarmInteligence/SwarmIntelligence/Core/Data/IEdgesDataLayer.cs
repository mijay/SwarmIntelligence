using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Data
{
	/// <summary>
	/// Class that represents the data associated with edges in current space.
	/// </summary>
	/// <typeparam name="TCoordinate">Type of coordinates in represented space.</typeparam>
	/// <typeparam name="TEdgeData">Type of data associated with edges between point in current space.</typeparam>
	[ContractClass(typeof(EdgesDataLayerContract<,>))]
	public interface IEdgesDataLayer<TCoordinate, TEdgeData>: ICompleteMapping<Edge<TCoordinate>, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		/// <summary>
		/// <see cref="Topology{TCoordinate}"/> of the current space.
		/// </summary>
		[Pure]
		Topology<TCoordinate> Topology { get; }
	}
}