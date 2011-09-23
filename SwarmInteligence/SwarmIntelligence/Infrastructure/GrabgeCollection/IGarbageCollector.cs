using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SwarmIntelligence.Infrastructure.GrabgeCollection
{
	[ContractClass(typeof(IGarbageCollectorContract<,,>))]
	public interface IGarbageCollector<TCoordinate, TNodeData, TEdgeData>
	{
		[Pure]
		MapBase<TCoordinate, TNodeData, TEdgeData> MapBase { get; }

		void AttachTo(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase);
		void Collect();
	}
}