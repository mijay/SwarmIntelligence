using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.MemoryManagement;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IGarbageCollector<,>))]
	public abstract class IGarbageCollectorContract<TKey, TValue>: IGarbageCollector<TKey, TValue>
	{
		#region Implementation of IGarbageCollector<TCoordinate,TNodeData,TEdgeData>

		public void Collect(MappingBase<TKey, TValue> mappingBase)
		{
			Contract.Requires(mappingBase != null);
			throw new UnreachableCodeException();
		}

		#endregion
	}
}