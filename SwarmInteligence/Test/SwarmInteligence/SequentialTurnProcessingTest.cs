using System;
using Common.Collections.Extensions;
using NUnit.Framework;
using SILibrary.Base;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.Playground;
using SwarmIntelligence.Specialized;

namespace Test.SwarmInteligence
{
	public class SequentialTurnProcessingTest: SwarmIntelligenceTestBase
	{
		#region Setup/Teardown

		public override void SetUp()
		{
			base.SetUp();
			InitializeWorld(new Coordinates2D(0, 0), new Coordinates2D(4, 4));
		}

		#endregion

		private class TestAnt: AntBase<Coordinates2D, EmptyData, EmptyData>
		{
			public TestAnt(World<Coordinates2D, EmptyData, EmptyData> world)
				: base(world)
			{
			}

			public event Action AntInCenterFound;

			#region Overrides of AntBase<Coordinates2D,EmptyData,EmptyData>

			public override void ProcessTurn()
			{
				var center = new Coordinates2D(2, 2);

				ICell<Coordinates2D, EmptyData, EmptyData> cell;
				IAnt<Coordinates2D, EmptyData, EmptyData> ant;
				if(World.Map.TryGet(center, out cell)
				   && cell.TrySingle(out ant))
					AntInCenterFound();
				else
					this.MoveTo(center);
			}

			#endregion
		}

		[Test]
		public void ƒва—уществаЌа арте_ќба»дут¬÷ентр_ќдин¬идит„то÷ентр”же«ан€т()
		{
			bool antInCenterFound = false;
			using(IMapModifier<Coordinates2D, EmptyData, EmptyData> modifier = world.GetModifier()) {
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
	}
}