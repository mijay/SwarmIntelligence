using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;

namespace SwarmIntelligence.MemoryManagement
{
	[ContractClass(typeof(IGarbageCollectorContract<,>))]
	public interface IGarbageCollector<TKey, TValue>
	{
		void Collect(MappingBase<TKey, TValue> mappingBase);
	}
}