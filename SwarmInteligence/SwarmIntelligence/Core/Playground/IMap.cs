using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Basic;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	[ContractClass(typeof(IMapContract<,,>))]
	public interface IMap<TCoordinate, TNodeData, TEdgeData>:
		ISparseMappint<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>,
		IEnumerableMapping<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>
	{
		[Pure]
		Topology<TCoordinate> Topology { get; }
	}
}