using System.Collections.Concurrent;

namespace Utils.Cache
{
    public class LocalCache: ConcurentDictionaryCache
    {
        public LocalCache(): base(new ConcurrentDictionary<object, object>()) {}
    }
}