using System;
using NUnit.Framework;
using Utils.Cache;

namespace Test2
{
    public class FuncCacherTest: CacheTestBase
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();
            funcCacher = new FuncCacher(localCache);
        }

        #endregion

        private FuncCacher funcCacher;

        [Test]
        public void CacheTakesFunctionIntoAccount()
        {
            const int key = 100;
            const decimal val1 = 897;
            const decimal val2 = 7;
            var func1 = GetFuncForCache(key, val1);
            var func2 = GetFuncForCache(key, val2);

            var cached1 = funcCacher.MakeCached(func1);
            var cached2 = funcCacher.MakeCached(func2);

            Assert.That(cached1(key), Is.EqualTo(val1));
            Assert.That(cached2(key), Is.EqualTo(val2));
            Assert.That(cached1(key), Is.EqualTo(val1));
            Assert.That(cached2(key), Is.EqualTo(val2));
        }

        [Test]
        public void CacheTakesArgumentsIntoAccount()
        {
            var keys = new[] { "key", "k", "j" };
            var vals = new[] { 5, 0, 345 };
            var used = new[] { false, false, false };
            Func<string, int> func = key => {
                                         int index = Array.IndexOf(keys, key);
                                         Assert.That(index, Is.GreaterThanOrEqualTo(0));
                                         Assert.That(used[index], Is.False);
                                         used[index] = true;
                                         return vals[index];
                                     };

            Func<string, int> cached = funcCacher.MakeCached(func);

            for(int i = 0; i < 3; ++i)
                for(int j = 0; j < keys.Length; ++j)
                    Assert.That(cached(keys[j]), Is.EqualTo(vals[j]));
        }

        [Test]
        public void CachedFuncCalledWithSameArgument_CachedValueReturned()
        {
            const string key = "key";
            const char value = '6';
            Func<string, char> func = GetFuncForCache(key, value);
            Func<string, char> cached = funcCacher.MakeCached(func);

            Assert.That(cached(key), Is.EqualTo(value));
            Assert.That(cached(key), Is.EqualTo(value));
        }
    }
}