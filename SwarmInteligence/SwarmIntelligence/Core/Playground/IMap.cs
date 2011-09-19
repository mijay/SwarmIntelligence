using SwarmIntelligence.Core.Basic;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	public interface IMap<TCoordinate, TNodeData, TEdgeData>:
		ISparseMappint<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>,
		IEnumerableMapping<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>
	{
		Topology<TCoordinate> Topology { get; }
	}
}