using System.Collections.Concurrent;

namespace Common.Cache
{
	public class LocalCache: ConcurentDictionaryCache
	{
		public LocalCache()
			: base(new ConcurrentDictionary<object, object>())
		{
		}
	}
}