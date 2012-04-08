using System;
using SwarmIntelligence.Core.Interfaces;

namespace SwarmIntelligence.MemoryManagement
{
	public interface IValueStorage<TKey, TValue>: IKeyValueContainer<TKey, TValue>
	{
		bool TryGet(TKey key, out TValue value);
		TValue GetOrCreate(TKey key, Func<TKey, TValue> builder);
	}
}