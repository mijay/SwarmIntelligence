using System.Collections;
using System.Collections.Generic;
using CommonTest;
using NUnit.Framework;

namespace Test.Common
{
    public class DITest: TestBase
    {
        [Test]
        public void Test1()
        {
            Assert.IsTrue(typeof(IList<int>).IsAssignableFrom(typeof(List<int>)));
            Assert.IsFalse(typeof(IList<>).IsAssignableFrom(typeof(List<int>)));
            Assert.IsTrue(typeof(IList).IsAssignableFrom(typeof(List<>)));
        }
    }
}