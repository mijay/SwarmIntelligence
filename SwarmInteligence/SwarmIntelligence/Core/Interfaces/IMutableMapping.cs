using System;

namespace SwarmIntelligence.Core.Interfaces
{
    public interface IMutableMapping<in TKey, TData>:IMapping<TKey, TData>
    {
		/// <summary>
		/// TODO
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <exception cref="IndexOutOfRangeException"><paramref name="key"/> value is illegal or out of the bounds.</exception>
        void Set(TKey key, TData data);
    }
}