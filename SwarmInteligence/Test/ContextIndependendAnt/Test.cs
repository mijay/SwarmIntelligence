using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common.Collections;
using CommonTest;
using NUnit.Framework;
using SILibrary.General.Background;
using SILibrary.General.Playground;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Specialized;

namespace Test.ContextIndependendAnt
{
	public class Test: TestBase
	{
		#region Setup/Teardown

		public override void SetUp()
		{
			base.SetUp();
			random = new Random();

			var topology = new EightConnectedSurfaceTopology(min, max);
			CellProvider<Coordinates2D, EmptyData, EmptyData> cellProvider = SetCell<Coordinates2D, EmptyData, EmptyData>.Provider();
			var map = new DictionaryMap<Coordinates2D, EmptyData, EmptyData>(topology, cellProvider);
			var nodeDataLayer = new EmptyDataLayer<Coordinates2D>();
			var edgeDataLayer = new EmptyDataLayer<Edge<Coordinates2D>>();

			world = new World<Coordinates2D, EmptyData, EmptyData>(nodeDataLayer, edgeDataLayer, map);

			runner = new Runner<Coordinates2D, EmptyData, EmptyData>(world);
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

		private World<Coordinates2D, EmptyData, EmptyData> world;
		private Random random;
		private Runner<Coordinates2D, EmptyData, EmptyData> runner;
		private readonly Coordinates2D min;
		private readonly Coordinates2D max;

		private IEnumerable<TestAnt> SeedAnts(int antsNumber, int timesAntJumps, params Coordinates2D[] lastAntSteps)
		{
			using(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier = world.Map.GetModifier())
				return EnumerableExtension
					.Repeat(() => SeedAnt(mapModifier, timesAntJumps, lastAntSteps), antsNumber)
					.ToArray();
		}

		private TestAnt SeedAnt(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier, int timesAntJumps, Coordinates2D[] lastAntSteps)
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

			KeyValuePair<Coordinates2D, ICell<Coordinates2D, EmptyData, EmptyData>> keyValuePair =
				world.Map.Single();
			Assert.That(keyValuePair.Key, Is.EqualTo(lastStep));
			CollectionAssert.AreEquivalent(seededAnts, keyValuePair.Value);
		}
	}
}