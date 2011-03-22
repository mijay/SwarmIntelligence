using System;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Test
{
    public class CacheTest: CacheTestBase
    {
        [Test]
        public void CacheCalledWithDifferentFactoryButSameKey_OnlyFirstFactoryCalled()
        {
            const string key1 = "one";
            const decimal val1 = 134;
            Func<string, decimal> func1 = GetFuncForCache(key1, val1);
            Func<string, decimal> func2 = delegate { throw new AssertionException("Incorrect func2 call"); };

            Assert.That(localCache.GetOrSet(key1, func1), Is.EqualTo(val1));
            Assert.That(localCache.GetOrSet(key1, func2), Is.EqualTo(val1));
            Assert.That(localCache.GetOrSet(key1, func1), Is.EqualTo(val1));
        }

        [Test]
        public void DifferentKeysUsed_MultipleCacheCallForEachKey_ResultIsCorrect()
        {
            Tuple<string> key1 = Tuple.Create("one");
            const decimal val1 = 134;
            Func<Tuple<string>, decimal> func1 = GetFuncForCache(key1, val1);

            Tuple<string, string> key2 = Tuple.Create("one", "two");
            const decimal val2 = 456;
            Func<Tuple<string, string>, decimal> func2 = GetFuncForCache(key2, val2);

            Assert.That(localCache.GetOrSet(key1, func1), Is.EqualTo(val1));
            Assert.That(localCache.GetOrSet(key2, func2), Is.EqualTo(val2));
            Assert.That(localCache.GetOrSet(key1, func1), Is.EqualTo(val1));
            Assert.That(localCache.GetOrSet(key2, func2), Is.EqualTo(val2));
        }

        [Test]
        public void MultipleCacheCall_OnlyOneFactoryCall()
        {
            Tuple<string> key = Tuple.Create("some");
            const decimal value = 134;
            Func<Tuple<string>, decimal> func = GetFuncForCache(key, value);

            Assert.That(localCache.GetOrSet(key, func), Is.EqualTo(value));
            Assert.That(localCache.GetOrSet(key, func), Is.EqualTo(value));
            Assert.That(localCache.GetOrSet(key, func), Is.EqualTo(value));
        }

        [Test]
        public void TwoKeysSameButNotEqual_CacheDoesntDifferThem()
        {
            const int key1 = 11;
            const decimal val1 = 67.7m;
            Func<int, decimal> func1 = GetFuncForCache(key1, val1);
            Assert.That(localCache.GetOrSet(key1, func1), Is.EqualTo(val1));

            int key2 = Math.Min(100, 1) + Enumerable.Range(0, 5).Sum();
            Assert.That(localCache.GetOrSet(key2, func1), Is.EqualTo(val1));
        }

        [Test]
        public void TwoKeysWithSameHashCode_CacheDiffersThem()
        {
            const int hashCode = 33;
            var key1 = new Mock<object>(MockBehavior.Loose);
            key1.Setup(x => x.GetHashCode()).Returns(hashCode);
            const decimal val1 = 134;
            Func<object, decimal> func1 = GetFuncForCache(key1.Object, val1);

            var key2 = new Mock<object>(MockBehavior.Loose);
            key2.Setup(x => x.GetHashCode()).Returns(hashCode);
            const decimal val2 = 666;
            Func<object, decimal> func2 = GetFuncForCache(key2.Object, val2);

            Assert.That(localCache.GetOrSet(key1.Object, func1), Is.EqualTo(val1));
            Assert.That(localCache.GetOrSet(key2.Object, func2), Is.EqualTo(val2));
        }
    }
}