using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;
using Common.Collections.Extensions;
using NUnit.Framework;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;
using SwarmIntelligence.Specialized;

namespace Test.SwarmInteligence.ContextIndependendAnt
{
	public class Test: SwarmIntelligenceTestBase
	{
		#region Setup/Teardown

		public override void SetUp()
		{
			base.SetUp();
			InitializeWorld(min, max);
			random = new Random();
		}

		#endregion

		public Test()
			: this(-4, -3, 12, 5)
		{
		}

		public Test(int minX, int minY, int maxX, int maxY)
		{
			min = new Coordinates2D(minX, minY);
			max = new Coordinates2D(maxX, maxY);
		}

		private Random random;
		private readonly Coordinates2D min;
		private readonly Coordinates2D max;

		private IEnumerable<TestAnt> SeedAnts(int antsNumber, int timesAntJumps, params Coordinates2D[] lastAntSteps)
		{
			using(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier = world.GetModifier())
				return EnumerableExtension
					.Repeat(() => SeedAnt(mapModifier, timesAntJumps, lastAntSteps), antsNumber)
					.ToArray();
		}

		private TestAnt SeedAnt(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier, int timesAntJumps,
		                        IEnumerable<Coordinates2D> lastAntSteps)
		{
			Coordinates2D initialPosition = GenerateCoordinate();
			var testAnt = new TestAnt(world, EnumerableExtension
			                                 	.Repeat(GenerateCoordinate, timesAntJumps)
			                                 	.Concat(lastAntSteps)
			                                 	.ToArray());
			mapModifier.AddAt(testAnt, initialPosition);
			return testAnt;
		}

		private Coordinates2D GenerateCoordinate()
		{
			int x = random.Next(min.x, max.x + 1);
			int y = random.Next(min.y, max.y + 1);
			return new Coordinates2D(x, y);
		}

		[Test]
		public void SimpleTest([Values(5, 10, 20, 80, 200)] int jumps, [Values(100, 1000)] int ants)
		{
			Coordinates2D lastStep = GenerateCoordinate();
			IEnumerable<TestAnt> seededAnts = SeedAnts(ants, jumps - 1, lastStep);
			var timer = new Stopwatch();
			timer.Start();
			for(int i = 0; i < jumps; ++i)
				runner.DoTurn();
			timer.Stop();
			Debug.WriteLine(string.Format("jumps - {0}; ants - {1}; time - {2} ms", jumps, ants, timer.ElapsedMilliseconds));

			ICell<Coordinates2D, EmptyData, EmptyData> cell = world.Map.Single();
			Assert.That(cell.Coordinate, Is.EqualTo(lastStep));
			CollectionAssert.AreEquivalent(seededAnts, cell);
		}

		private class TestAnt : AntBase<Coordinates2D, EmptyData, EmptyData>
		{
			private readonly Queue<Coordinates2D> points;

			public TestAnt(World<Coordinates2D, EmptyData, EmptyData> world, params Coordinates2D[] points)
				: base(world)
			{
				Contract.Requires(points != null);
				this.points = new Queue<Coordinates2D>(points);
			}

			#region Overrides of IAnt<Coordinates2D,EmptyData,EmptyData>

			public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
			{
				this.MoveTo(points.Dequeue());
			}

			#endregion
		}
	}
}