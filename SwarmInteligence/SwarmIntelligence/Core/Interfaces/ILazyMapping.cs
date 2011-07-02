using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SwarmIntelligence.Core.Interfaces
{
    [ContractClass(typeof(ContractILazyMapping<,>))]
    public interface ILazyMapping<TKey, TData>: IMapping<TKey, TData>
    {
        [Pure]
        bool IsInitialized(TKey key);

        [Pure]
        IEnumerable<KeyValuePair<TKey, TData>> GetInitialized();

        void Free(TKey key);
    }

    [ContractClassFor(typeof(ILazyMapping<,>))]
    internal abstract class ContractILazyMapping<TKey, TData>: ILazyMapping<TKey, TData>
    {
        #region Implementation of ILazyMapping<C,TData>

        public bool IsInitialized(TKey key)
        {
            throw new NotImplementedException();
        }

        public void Free(TKey key)
        {
            Contract.Requires(IsInitialized(key));
            Contract.Ensures(!IsInitialized(key));

            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<TKey, TData>> GetInitialized()
        {
            Contract.Ensures(
                Contract.ForAll(
                    Contract.Result<IEnumerable<KeyValuePair<TKey, TData>>>(),
                    pair => IsInitialized(pair.Key) && this[pair.Key].Equals(pair.Value)));

            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IMapping<C,out TData>

        public TData this[TKey key]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}