using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace Test.ContextIndependendAnt
{
	public class TestAnt: AntBase<Coordinates2D, EmptyData, EmptyData>
	{
		private readonly Queue<Coordinates2D> points;

		public TestAnt(World<Coordinates2D, EmptyData, EmptyData> world, params Coordinates2D[] points)
			: base(world)
		{
			Contract.Requires(points != null);
			this.points = new Queue<Coordinates2D>(points);
		}

		#region Overrides of Ant<Coordinates2D,EmptyData,EmptyData>

		protected override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
		{
			outlook.MoveTo(points.Dequeue());
		}

		#endregion
	}
}