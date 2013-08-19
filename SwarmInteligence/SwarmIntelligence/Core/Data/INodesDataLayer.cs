using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Data
{
	/// <summary>
	/// Class that represents the data associated with nodes in current space.
	/// </summary>
	/// <typeparam name="TCoordinate">Type of coordinates in represented space.</typeparam>
	/// <typeparam name="TNodeData">Type of data associated with edges between point in current space.</typeparam>
	[ContractClass(typeof(INodesDataLayerContract<,>))]
	public interface INodesDataLayer<TCoordinate, TNodeData>: ICompleteMapping<TCoordinate, TNodeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		/// <summary>
		/// <see cref="Topology{TCoordinate}"/> of the current space.
		/// </summary>
		[Pure]
		Topology<TCoordinate> Topology { get; }
	}
}