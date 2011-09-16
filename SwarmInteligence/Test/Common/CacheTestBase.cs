using System;
using System.Collections.Concurrent;
using Common.Cache;
using CommonTest;
using NUnit.Framework;

namespace Test.Common
{
	public class CacheTestBase: TestBase
	{
		#region Setup/Teardown

		public override void SetUp()
		{
			base.SetUp();
			localCache = new ConcurentDictionaryCache(new ConcurrentDictionary<object, object>());
		}

		#endregion

		protected ConcurentDictionaryCache localCache;

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