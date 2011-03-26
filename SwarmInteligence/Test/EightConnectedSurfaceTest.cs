using CommonTest;
using NUnit.Framework;
using SILibrary.TwoDimensional;

namespace Test
{
    public class EightConnectedSurfaceTest: TestBase
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();

            var boundaries2D = new Boundaries2D(new Coordinates2D(2, 2), new Coordinates2D(10, 10));
            topology = new EightConnectedSurfaceTopology(boundaries2D);
        }

        #endregion

        private EightConnectedSurfaceTopology topology;

        [Test]
        public void BorderConditions()
        {
            Assert.Throws(CommonTest.Iz.Any, () => topology.GetSuccessors(new Coordinates2D(0, 0)));

            CollectionAssert.AreEquivalent(
                new[] { new Coordinates2D(2, 3), new Coordinates2D(3, 3), new Coordinates2D(3, 2) },
                topology.GetSuccessors(new Coordinates2D(2, 2)));

            CollectionAssert.AreEquivalent(
                new[]
                {
                    new Coordinates2D(2, 2), new Coordinates2D(2, 3), new Coordinates2D(3, 3),
                    new Coordinates2D(4, 3), new Coordinates2D(4, 2)
                },
                topology.GetSuccessors(new Coordinates2D(3, 2)));

            CollectionAssert.AreEquivalent(
                new[]
                {
                    new Coordinates2D(2, 2), new Coordinates2D(3, 2), new Coordinates2D(3, 3),
                    new Coordinates2D(3, 4), new Coordinates2D(2, 4)
                },
                topology.GetSuccessors(new Coordinates2D(2, 3)));
        }
    }
}