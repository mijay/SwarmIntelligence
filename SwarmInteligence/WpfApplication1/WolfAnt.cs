using System;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;

namespace WpfApplication1
{
	public class WolfAnt: Animal
	{
		private const double MassPrey = 0.5;

		public WolfAnt(World<Coordinates2D, EmptyData, EmptyData> world, int weight)
			: base(world, weight, 3)
		{
		}

		public override void ProcessTurn()
		{
			ICell<Coordinates2D, EmptyData, EmptyData>[] cellsWithPreys = Cell
				.GetSuburbCells(speed)
				.Where(cell => cell.OfType<PreyAnt>().IsNotEmpty())
				.ToArray();

			if(cellsWithPreys.IsNotEmpty()) {
				ICell<Coordinates2D, EmptyData, EmptyData> target = cellsWithPreys.First();
				EatPreysAt(target);
			} else {
				Weight--;
				Coordinates2D[] allCells = Cell
					.GetSuburb(speed)
					.ToArray();
				Coordinates2D cellToGo = allCells[Random.Next(allCells.Length)];
				this.MoveTo(cellToGo);
			}

			if(Weight == 6) {
				Weight = 3;
				AddCloneToEmptyCell();
			} else if(Weight <= 0)
				this.RemoveFrom(Coordinate, this);
		}

		private void AddCloneToEmptyCell()
		{
			Coordinates2D[] emptyCells = GetEmptySuburbCells(reproductionRadius);
			Coordinates2D cellToAddTo = emptyCells[Random.Next(emptyCells.Length)];
			this.AddTo(new WolfAnt(World, Weight), cellToAddTo);
		}

		private void EatPreysAt(ICell<Coordinates2D, EmptyData, EmptyData> target)
		{
			double preyWeight = 0;
			foreach(PreyAnt preyAnt in target.OfType<PreyAnt>()) {
				preyWeight += preyAnt.Weight;
				this.RemoveFrom(target.Coordinate, preyAnt);
			}
			Weight = (int) Math.Min(6, preyWeight * MassPrey + Weight);
			this.MoveTo(target.Coordinate);
		}
	}
}