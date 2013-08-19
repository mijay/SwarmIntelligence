using Common.Collections.Extensions;
using CommonTest;
using NUnit.Framework;

namespace Test.Common
{
	public class EnumerableTest: TestBase
	{
		[Test]
		public void OneSetIsEmpty_SetMultiply()
		{
			var a = new[] { 1, 2, 3 };
			var b = new string[0];

			Assert.That(a.SetMultiply(b, (i, s) => s + i), Is.Empty);
		}

		[Test]
		public void TestSetMultiply()
		{
			var a = new[] { 1, 2, 3 };
			var b = new[] { "a", "b" };

			CollectionAssert.AreEquivalent(
				a.SetMultiply(b, (i, s) => s + i),
				new[] { "a1", "a2", "a3", "b1", "b2", "b3" });
		}
	}
}