using System;

namespace Common.Cache
{
	public interface IKeyValueCache
	{
		TVal GetOrAdd<TKey, TVal>(TKey key, Func<TKey, TVal> factory);
		void AddOrUpdate<TKey, TVal>(TKey key, TVal value);
		void Remove<TKey>(TKey key);
	}
}