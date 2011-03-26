using System;
using System.Collections.Concurrent;

namespace Common.Cache
{
    public abstract class ConcurentDictionaryCache: IKeyValueCache
    {
        private readonly ConcurrentDictionary<object, object> cache;

        protected ConcurentDictionaryCache(ConcurrentDictionary<object, object> cache)
        {
            this.cache = cache;
        }

        #region IKeyValueCache Members

        public TVal GetOrSet<TKey, TVal>(TKey key, Func<TKey, TVal> factory)
        {
            if(key == null)
                throw new ArgumentNullException("key");
            object result = cache.GetOrAdd(key, objKey => factory((TKey) objKey));
            if(!(result is TVal))
                throw new ArgumentException(string.Format("The data stored for the key {0} has type {1} when type {2} expected.",
                                                          key, result.GetType().FullName, typeof(TVal).FullName));
            return (TVal) result;
        }

        #endregion
    }
}