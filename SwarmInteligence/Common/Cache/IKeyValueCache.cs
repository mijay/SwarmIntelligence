using System;

namespace Common.Cache
{
    public interface IKeyValueCache
    {
        TVal GetOrSet<TKey, TVal>(TKey key, Func<TKey, TVal> factory);
    }
}