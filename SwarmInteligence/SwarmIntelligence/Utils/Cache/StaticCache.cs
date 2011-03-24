using System.Collections.Concurrent;

namespace SwarmIntelligence.Utils.Cache
{
    public class StaticCache: ConcurentDictionaryCache
    {
        protected static readonly ConcurrentDictionary<object, object> cache =
            new ConcurrentDictionary<object, object>();

        public StaticCache(): base(cache) {}
    }
}