using System.Collections.Concurrent;

namespace Common.Cache
{
	public class StaticCache: ConcurrentDictionaryCache
	{
		protected static readonly ConcurrentDictionary<object, object> cache =
			new ConcurrentDictionary<object, object>();

		public StaticCache()
			: base(cache)
		{
		}
	}
}