using System;
using CommonTest;
using NUnit.Framework;
using SILibrary.Common;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.GrabgeCollection;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Infrastructure.TurnProcessing;
using Common.Collections;
using SwarmIntelligence.Specialized;

namespace Test.SwarmInteligence
{
	public class SequentialTurnProcessingTest: TestBase
	{
		private World<Coordinates2D, EmptyData, EmptyData> world;
		private Runner<Coordinates2D, EmptyData, EmptyData> runner;

		#region Setup/Teardown

		public override void SetUp()
		{
			base.SetUp();
			var logger = new Logger();
			var topology = new FourConnectedSurfaceTopology(new Coordinates2D(0, 0), new Coordinates2D(4, 4));
			CellProvider<Coordinates2D, EmptyData, EmptyData> cellProvider = SetCell<Coordinates2D, EmptyData, EmptyData>.Provider();
			var map = new DictionaryMap<Coordinates2D, EmptyData, EmptyData>(topology, cellProvider, logger);
			var nodeDataLayer = new EmptyDataLayer<Coordinates2D>();
			var edgeDataLayer = new EmptyDataLayer<Edge<Coordinates2D>>();
			world = new World<Coordinates2D, EmptyData, EmptyData>(nodeDataLayer, edgeDataLayer, map, logger);
			runner = new Runner<Coordinates2D, EmptyData, EmptyData>(world, new GarbageCollector<Coordinates2D, EmptyData, EmptyData>());
		}

		#endregion

		[Test]
		public void ƒва—уществаЌа арте_ќба»дут¬÷ентр_ќдин¬идит„то÷ентр”же«ан€т()
		{
			var antInCenterFound = false;
			using(var modifier = world.Map.GetModifier()) {
				var ant = new TestAnt(world);
				ant.AntInCenterFound += () => {
				                        	Assert.IsFalse(antInCenterFound);
				                        	antInCenterFound = true;
				                        };
				modifier.AddAt(ant, new Coordinates2D(1, 1));
				ant = new TestAnt(world);
				ant.AntInCenterFound += () => {
				                        	Assert.IsFalse(antInCenterFound);
				                        	antInCenterFound = true;
				                        };
				modifier.AddAt(ant, new Coordinates2D(3, 3));
			}

			runner.DoTurn();

			Assert.IsTrue(antInCenterFound);
		}

		public class TestAnt: AntBase<Coordinates2D, EmptyData, EmptyData>
		{
			public TestAnt(World<Coordinates2D, EmptyData, EmptyData> world)
				: base(world)
			{
			}

			public event Action AntInCenterFound;

			#region Overrides of AntBase<Coordinates2D,EmptyData,EmptyData>

			public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
			{
				var center = new Coordinates2D(2, 2);

				ICell<Coordinates2D, EmptyData, EmptyData> cell;
				IAnt<Coordinates2D, EmptyData, EmptyData> ant;
				if (outlook.Map.TryGet(center, out cell)
					&& cell.TrySingle(out ant))
					AntInCenterFound();
				else
					this.MoveTo(center);
			}

			#endregion
		}
	}
}