using System;

namespace SwarmIntelligence.Core
{
	public abstract class DataLayer<TKey, TData>
	{
		public TData this[TKey key]
		{
			get { return Get(key); }
			set { Set(key, value); }
		}

		public abstract TData Get(TKey key);

		/// <exception cref="NotSupportedException">Method is not supported in current implementation.</exception>
		public abstract void Set(TKey key, TData data);
	}
}