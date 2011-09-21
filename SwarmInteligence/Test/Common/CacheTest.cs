using System;
using System.Linq;
using Common;
using Moq;
using NUnit.Framework;

namespace Test.Common
{
	public class CacheTest: CacheTestBase
	{
		[Test]
		public void AddOrUpdateWorks()
		{
			const string key = "a";
			const int value = 1;
			const int value2 = 2;

			localCache.AddOrUpdate(key, value);
			Assert.That(localCache.GetOrAdd<string, int>(key, _ => { throw new Exception(); }), Is.EqualTo(value));

			localCache.AddOrUpdate(key, value2);
			Assert.That(localCache.GetOrAdd<string, int>(key, _ => { throw new Exception(); }), Is.EqualTo(value2));
		}

		[Test]
		public void CacheCalledWithDifferentFactoryButSameKey_OnlyFirstFactoryCalled()
		{
			const string key1 = "one";
			const decimal val1 = 134;
			Func<string, decimal> func1 = GetFuncForSingleCall(key1, val1);
			Func<string, decimal> func2 = delegate { throw new AssertionException("Incorrect func2 call"); };

			Assert.That(localCache.GetOrAdd(key1, func1), Is.EqualTo(val1));
			Assert.That(localCache.GetOrAdd(key1, func2), Is.EqualTo(val1));
			Assert.That(localCache.GetOrAdd(key1, func1), Is.EqualTo(val1));
		}

		[Test]
		public void DifferentKeysUsed_MultipleCacheCallForEachKey_ResultIsCorrect()
		{
			Tuple<string> key1 = Tuple.Create("one");
			const decimal val1 = 134;
			Func<Tuple<string>, decimal> func1 = GetFuncForSingleCall(key1, val1);

			Tuple<string, string> key2 = Tuple.Create("one", "two");
			const decimal val2 = 456;
			Func<Tuple<string, string>, decimal> func2 = GetFuncForSingleCall(key2, val2);

			Assert.That(localCache.GetOrAdd(key1, func1), Is.EqualTo(val1));
			Assert.That(localCache.GetOrAdd(key2, func2), Is.EqualTo(val2));
			Assert.That(localCache.GetOrAdd(key1, func1), Is.EqualTo(val1));
			Assert.That(localCache.GetOrAdd(key2, func2), Is.EqualTo(val2));
		}

		[Test]
		public void KeyIsNotInCache_RemoveIt_NoExceptionOccures()
		{
			Assert.DoesNotThrow(() => localCache.Remove("12"));
		}

		[Test]
		public void MultipleCacheCall_OnlyOneFactoryCall()
		{
			Tuple<string> key = Tuple.Create("some");
			const decimal value = 134;
			Func<Tuple<string>, decimal> func = GetFuncForSingleCall(key, value);

			Assert.That(localCache.GetOrAdd(key, func), Is.EqualTo(value));
			Assert.That(localCache.GetOrAdd(key, func), Is.EqualTo(value));
			Assert.That(localCache.GetOrAdd(key, func), Is.EqualTo(value));
		}

		[Test]
		public void RemoveOneKey_OtherIsNotAffected()
		{
			const string key1 = "12";
			const string key2 = "123";
			const decimal value1 = 178;
			const decimal value2 = 78;
			Func<string, decimal> func1 = GetFuncForSingleCall(key1, value1);
			localCache.GetOrAdd(key1, func1);
			Func<string, decimal> func2 = GetFuncForSingleCall(key2, value2);
			localCache.GetOrAdd(key2, func2);

			localCache.Remove(key2);
			Assert.That(localCache.GetOrAdd(key1, func1), Is.EqualTo(value1));
		}

		[Test]
		public void RemoveWorks()
		{
			const string key = "12";
			const decimal value = 178;
			localCache.GetOrAdd(key, _ => value);

			localCache.Remove(key);

			MutableTuple<bool> wasCalled;
			Func<string, decimal> func = GetFuncForSingleCall(key, value, out wasCalled);
			Assert.That(localCache.GetOrAdd(key, func), Is.EqualTo(value));
			Assert.That(wasCalled.Item1);
		}

		[Test]
		public void TwoKeysSameButNotEqual_CacheDoesntDifferThem()
		{
			const int key1 = 11;
			const decimal val1 = 67.7m;
			Func<int, decimal> func1 = GetFuncForSingleCall(key1, val1);
			Assert.That(localCache.GetOrAdd(key1, func1), Is.EqualTo(val1));

			int key2 = Math.Min(100, 1) + Enumerable.Range(0, 5).Sum();
			Assert.That(localCache.GetOrAdd(key2, func1), Is.EqualTo(val1));
		}

		[Test]
		public void TwoKeysWithSameHashCode_CacheDiffersThem()
		{
			const int hashCode = 33;
			var key1 = new Mock<object>(MockBehavior.Loose);
			key1.Setup(x => x.GetHashCode()).Returns(hashCode);
			const decimal val1 = 134;
			Func<object, decimal> func1 = GetFuncForSingleCall(key1.Object, val1);

			var key2 = new Mock<object>(MockBehavior.Loose);
			key2.Setup(x => x.GetHashCode()).Returns(hashCode);
			const decimal val2 = 666;
			Func<object, decimal> func2 = GetFuncForSingleCall(key2.Object, val2);

			Assert.That(localCache.GetOrAdd(key1.Object, func1), Is.EqualTo(val1));
			Assert.That(localCache.GetOrAdd(key2.Object, func2), Is.EqualTo(val2));
		}
	}
}