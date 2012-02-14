using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SwarmIntelligence.Infrastructure.GrabgeCollection
{
	[ContractClass(typeof(IGarbageCollectorContract<,,>))]
	public interface IGarbageCollector<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		[Pure]
		MapBase<TCoordinate, TNodeData, TEdgeData> MapBase { get; }

		void AttachTo(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase);
		void Collect();
	}
}