using SwarmIntelligence.Core.Interfaces;

namespace SwarmIntelligence.Core
{
	public abstract class DataLayer<TKey, TData>: IMutableMapping<TKey, TData>
	{
		public TData this[TKey key]
		{
			get { return Get(key); }
			set { Set(key, value); }
		}

		#region Implementation of IMapping<in TKey,out TData>

		public abstract TData Get(TKey key);

		#endregion

		#region Implementation of IMutableMapping<in TKey,TData>

		public abstract void Set(TKey key, TData data);

		#endregion
	}
}