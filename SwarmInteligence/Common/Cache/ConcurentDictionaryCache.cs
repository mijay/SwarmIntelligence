using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;

namespace Common.Cache
{
    public class ConcurentDictionaryCache: IKeyValueCache
    {
        private readonly ConcurrentDictionary<object, object> cache;

        public ConcurentDictionaryCache(ConcurrentDictionary<object, object> cache)
        {
            this.cache = cache;
        }

        #region IKeyValueCache Members

        public TVal GetOrSet<TKey, TVal>(TKey key, Func<TKey, TVal> factory)
        {
            Contract.Requires(factory != null && key != null);
            object result = cache.GetOrAdd(key, objKey => factory((TKey) objKey));
            if(!(result is TVal))
                throw new ArgumentException(string.Format("The data stored for the key {0} has type {1} when type {2} expected.",
                                                          key, result.GetType().FullName, typeof(TVal).FullName));
            return (TVal) result;
        }

        #endregion
    }
}