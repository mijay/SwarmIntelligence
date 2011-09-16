using System;
using Common.Cache;
using SwarmIntelligence.Core;

namespace SILibrary.General.Background
{
	public class DelegateDataLayer<TKey, TData>: DataLayer<TKey, TData>
	{
		private readonly Func<TKey, TData> factory;
		private readonly IKeyValueCache keyValueCache;

		public DelegateDataLayer(Func<TKey, TData> factory, IKeyValueCache keyValueCache)
		{
			this.factory = factory;
			this.keyValueCache = keyValueCache;
		}

		#region Overrides of DataLayer<TKey,TData>

		public override TData Get(TKey key)
		{
			return keyValueCache.GetOrAdd(Tuple.Create(this, key), x => factory(x.Item2));
		}

		public override void Set(TKey key, TData data)
		{
			keyValueCache.AddOrUpdate(Tuple.Create(this, key), data);
		}

		#endregion
	}
}