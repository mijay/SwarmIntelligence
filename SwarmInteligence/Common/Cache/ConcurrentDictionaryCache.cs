﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;

namespace Common.Cache
{
	public class ConcurrentDictionaryCache: IKeyValueCache
	{
		private readonly ConcurrentDictionary<object, object> cache;

		public ConcurrentDictionaryCache(ConcurrentDictionary<object, object> cache)
		{
			Contract.Requires(cache != null);
			this.cache = cache;
		}

		#region IKeyValueCache Members

		public TVal GetOrAdd<TKey, TVal>(TKey key, Func<TKey, TVal> factory)
		{
			object result = cache.GetOrAdd(key, objKey => factory((TKey) objKey));
			if(!(result is TVal))
				throw new ArgumentException(string.Format("The data stored for the key {0} has type {1} when type {2} expected.",
				                                          key, result.GetType().FullName, typeof(TVal).FullName));
			return (TVal) result;
		}

		public void AddOrUpdate<TKey, TVal>(TKey key, TVal value)
		{
			cache.AddOrUpdate(key, value, (_, oldVal) => value);
		}

		public void Remove<TKey>(TKey key)
		{
			object _;
			cache.TryRemove(key, out _);
		}

		#endregion
	}
}