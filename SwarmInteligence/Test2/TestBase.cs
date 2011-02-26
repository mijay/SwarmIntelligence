using NUnit.Framework;

namespace Test2
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