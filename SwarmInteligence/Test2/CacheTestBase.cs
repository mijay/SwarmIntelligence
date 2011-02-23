using System;
using NUnit.Framework;
using Utils.Cache;

namespace Test2
{
    public class CacheTestBase: TestBase
    {
        protected LocalCache localCache;

        public override void SetUp()
        {
            base.SetUp();
            localCache = new LocalCache();
        }

        protected static Func<TKey, TVal> GetFuncForCache<TKey, TVal>(TKey key, TVal value)
        {
            bool firstCall = true;
            return a => {
                       Assert.IsTrue(firstCall);
                       firstCall = false;
                       Assert.That(a, Is.EqualTo(key));
                       return value;
                   };
        }
    }
}