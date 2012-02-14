using System.Linq;
using NUnit.Framework;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;
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

			public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
			{
				Validate(outlook, new Coordinates2D(3, 3));
				this.MoveTo(new Coordinates2D(1, 1));
				Validate(outlook, new Coordinates2D(1, 1));
				this.MoveTo(new Coordinates2D(2, 0));
				Validate(outlook, new Coordinates2D(2, 0));
			}

			private void Validate(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook, Coordinates2D coordinate)
			{
				Assert.AreEqual(outlook.Me, this);
				ICell<Coordinates2D, EmptyData, EmptyData> cell;
				Assert.That(outlook.Map.TryGet(outlook.Coordinate, out cell));
				Assert.AreEqual(outlook.Cell, cell);
				Assert.That(cell.Contains(this));
				Assert.AreEqual(cell.Coordinate, outlook.Coordinate);
				Assert.AreEqual(cell.Coordinate, coordinate);
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