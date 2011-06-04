using System;
using System.Diagnostics.Contracts;

namespace Common.Cache
{
    [ContractClass(typeof(IKeyValueCacheContract))]
    public interface IKeyValueCache
    {
        TVal GetOrAdd<TKey, TVal>(TKey key, Func<TKey, TVal> factory);
    }

    [ContractClassFor(typeof(IKeyValueCache))]
    internal abstract class IKeyValueCacheContract: IKeyValueCache
    {
        #region Implementation of IKeyValueCache

        public TVal GetOrAdd<TKey, TVal>(TKey key, Func<TKey, TVal> factory)
        {
            Contract.Requires(factory != null && key != null);
            throw new NotSupportedException();
        }

        #endregion
    }
}