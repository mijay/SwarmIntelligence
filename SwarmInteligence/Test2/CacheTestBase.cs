using System;
using NUnit.Framework;
using Utils.Cache;

namespace Test2
{
    public class CacheTestBase: TestBase
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();
            localCache = new LocalCache();
        }

        #endregion

        protected LocalCache localCache;

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