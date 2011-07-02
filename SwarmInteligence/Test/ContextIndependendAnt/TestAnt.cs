using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence.Core.Creatures;

namespace Test.ContextIndependendAnt
{
    public class TestAnt: Ant<Coordinates2D, EmptyData, EmptyData>
    {
        private readonly Queue<Coordinates2D> points;

        public TestAnt(params Coordinates2D[] points)
        {
            Contract.Requires(points != null);
            this.points = new Queue<Coordinates2D>(points);
        }

        #region Overrides of Ant<Coordinates2D,NoDataBackground>

        public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
        {
            outlook.MoveTo(points.Dequeue());
        }

        #endregion
    }
}