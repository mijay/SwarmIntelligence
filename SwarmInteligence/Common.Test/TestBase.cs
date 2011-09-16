using NUnit.Framework;

namespace CommonTest
{
	[TestFixture]
	public abstract class TestBase
	{
		#region Setup/Teardown

		[SetUp]
		public virtual void SetUp() {}

		#endregion

		[TestFixtureSetUp]
		public virtual void FixtureSetUp() {}
	}
}