using System;
using System.Collections.Concurrent;
using Common;
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
			localCache = new ConcurrentDictionaryCache(new ConcurrentDictionary<object, object>());
		}

		#endregion

		protected ConcurrentDictionaryCache localCache;

		protected static Func<TKey, TVal> GetFuncForSingleCall<TKey, TVal>(TKey key, TVal value)
		{
			bool firstCall = true;
			return a => {
			       	Assert.IsTrue(firstCall);
			       	firstCall = false;
			       	Assert.That(a, Is.EqualTo(key));
			       	return value;
			       };
		}

		protected static Func<TKey, TVal> GetFuncForSingleCall<TKey, TVal>(TKey key, TVal value, out MutableTuple<bool> wasCalled)
		{
			wasCalled = MutableTuple.Create(false);
			MutableTuple<bool> wasCalledLocal = wasCalled;
			return a => {
			       	Assert.IsFalse(wasCalledLocal.Item1);
			       	wasCalledLocal.Item1 = true;
			       	Assert.That(a, Is.EqualTo(key));
			       	return value;
			       };
		}
	}
}