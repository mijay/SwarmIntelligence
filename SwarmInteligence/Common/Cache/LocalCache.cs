using System.Collections.Concurrent;

namespace Common.Cache
{
	public class LocalCache: ConcurrentDictionaryCache
	{
		public LocalCache()
			: base(new ConcurrentDictionary<object, object>())
		{
		}
	}
}