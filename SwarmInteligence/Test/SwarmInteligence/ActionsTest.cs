using System.Linq;
using NUnit.Framework;
using SILibrary.Empty;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.Specialized;

namespace Test.SwarmInteligence
{
	public class ActionsTest: SwarmIntelligenceTestBase
	{
		#region Setup/Teardown

		public override void SetUp()
		{
			base.SetUp();
			InitializeWorld(new Coordinates2D(0, 0), new Coordinates2D(4, 4));
		}

		#endregion

		public class TestAnt: AntBase<Coordinates2D, EmptyData, EmptyData>
		{
			public TestAnt(World<Coordinates2D, EmptyData, EmptyData> world)
				: base(world)
			{
			}

			#region Overrides of AntBase<Coordinates2D,EmptyData,EmptyData>

			protected override void DoProcessTurn()
			{
				Validate(new Coordinates2D(3, 3));
				this.MoveTo(new Coordinates2D(1, 1));
				Validate(new Coordinates2D(1, 1));
				this.MoveTo(new Coordinates2D(2, 0));
				Validate(new Coordinates2D(2, 0));
			}

			private void Validate(Coordinates2D coordinate)
			{
				Assert.AreEqual(Coordinate, coordinate);
				ICell<Coordinates2D, EmptyData, EmptyData> cell;
				Assert.That(World.Map.TryGet(Coordinate, out cell));
				Assert.AreEqual(Cell, cell);
				Assert.That(cell.Contains(this));
			}

			#endregion
		}

		[Test]
		public void AntCanMoveTwiceInATurn()
		{
			using(IMapModifier<Coordinates2D, EmptyData, EmptyData> modifier = world.GetModifier())
				modifier.AddAt(new TestAnt(world), new Coordinates2D(3, 3));
			runner.DoTurn();
		}
	}
}