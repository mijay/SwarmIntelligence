using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core;
using SwarmIntelligence2.Core.Commands;
using SwarmIntelligence2.TwoDimensional;

namespace Test2.BasicTest
{
    public class TestAnt: Ant<Coordinates2D, NoDataBackground>
    {
        private readonly Queue<Coordinates2D> points;

        public TestAnt(params Coordinates2D[] points)
        {
            Contract.Requires<ArgumentNullException>(points != null);
            this.points = new Queue<Coordinates2D>(points);
        }

        #region Overrides of Ant<Coordinates2D,NoDataBackground>

        public override IEnumerable<Command<Coordinates2D, NoDataBackground>> ProcessTurn()
        {
            Contract.Requires<IndexOutOfRangeException>(points.Count > 0);
            return new[] { new MoveTo<Coordinates2D, NoDataBackground>(points.Dequeue()) };
        }

        #endregion
    }
}