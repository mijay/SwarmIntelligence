using Common.Memoization;
using SwarmIntelligence.Core;

namespace SILibrary.General.Background
{
	public class CachedDataLayer<TKey, TData>: DataLayer<TKey, TData>
	{
		private readonly IMemoizedFunc<TKey, TData> cachedGet;
		private readonly DataLayer<TKey, TData> dataLayer;

		public CachedDataLayer(DataLayer<TKey, TData> dataLayer, IMemoizer memoizer)
		{
			this.dataLayer = dataLayer;
			cachedGet = memoizer.Memoize<TKey, TData>(c => dataLayer[c]);
		}

		#region Overrides of DataLayer<TKey,TData>

		public override TData Get(TKey key)
		{
			return cachedGet.Get(key);
		}

		public override void Set(TKey key, TData data)
		{
			dataLayer[key] = data;
			cachedGet.Refresh(key);
		}

		#endregion
	}
}