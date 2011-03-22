using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Commands;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.GeneralImplementation.Background;
using SwarmIntelligence.TwoDimensional;

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

        public override IEnumerable<Command<Coordinates2D, EmptyData, EmptyData>> ProcessTurn()
        {
            return new[] { new MoveTo<Coordinates2D, EmptyData, EmptyData>(points.Dequeue()) };
        }

        #endregion
    }
}