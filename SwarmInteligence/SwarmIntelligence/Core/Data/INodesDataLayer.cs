using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Data
{
	/// <summary>
	/// Class that represents the data associated with nodes in current space.
	/// </summary>
	/// <typeparam name="TCoordinate">Type of coordinates in represented space.</typeparam>
	/// <typeparam name="TNodeData">Type of data associated with edges between point in current space.</typeparam>
	public interface INodesDataLayer<TCoordinate, TNodeData>: IDataLayer<TCoordinate, TCoordinate, TNodeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
	}
}