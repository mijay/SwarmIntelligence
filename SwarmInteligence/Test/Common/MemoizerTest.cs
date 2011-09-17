using System;
using Common.Memoization;
using NUnit.Framework;

namespace Test.Common
{
	public class MemoizerTest: CacheTestBase
	{
		#region Setup/Teardown

		public override void SetUp()
		{
			base.SetUp();
			memoizer = new Memoizer(localCache);
		}

		#endregion

		private Memoizer memoizer;

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

			IMemoizedFunc<string, int> cached = memoizer.Memoize(func);

			for(int i = 0; i < 3; ++i)
				for(int j = 0; j < keys.Length; ++j)
					Assert.That(cached.Get(keys[j]), Is.EqualTo(vals[j]));
		}

		[Test]
		public void CacheTakesFunctionIntoAccount()
		{
			const int key = 100;
			const decimal val1 = 897;
			const decimal val2 = 7;
			Func<int, decimal> func1 = GetFuncForCache(key, val1);
			Func<int, decimal> func2 = GetFuncForCache(key, val2);

			IMemoizedFunc<int, decimal> cached1 = memoizer.Memoize(func1);
			IMemoizedFunc<int, decimal> cached2 = memoizer.Memoize(func2);

			Assert.That(cached1.Get(key), Is.EqualTo(val1));
			Assert.That(cached2.Get(key), Is.EqualTo(val2));
			Assert.That(cached1.Get(key), Is.EqualTo(val1));
			Assert.That(cached2.Get(key), Is.EqualTo(val2));
		}

		[Test]
		public void CachedFuncCalledWithSameArgument_CachedValueReturned()
		{
			const string key = "key";
			const char value = '6';
			Func<string, char> func = GetFuncForCache(key, value);
			IMemoizedFunc<string, char> cached = memoizer.Memoize(func);

			Assert.That(cached.Get(key), Is.EqualTo(value));
			Assert.That(cached.Get(key), Is.EqualTo(value));
		}

		[Test]
		public void RefreshWorks()
		{
			Assert.Fail();
		}
	}
}