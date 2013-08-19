using SILibrary.Empty;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using Common.Collections.Extensions;

namespace WolfsAndPreysWpf
{
	public class PreyAnt: Animal
	{
		public PreyAnt(World<Coordinates2D, EmptyData, EmptyData> world, int weight)
			: base(world, weight, 2)
		{
		}

		protected override void DoProcessTurn()
		{
			MoveToCellWithoutWolf(speed);

			if(Weight < 6)
				Weight++;
			else {
				Weight = 3;
				AddCloneToEmptyCell(reproductionRadius);
			}
		}

		private void MoveToCellWithoutWolf(int radius)
		{
			Coordinates2D[] cellsWithouWolfs = CellWithoutAnimalOfType<WolfAnt>(radius);
			if(cellsWithouWolfs.IsEmpty())
				return;
			Coordinates2D cellToGo = cellsWithouWolfs[Random.Next(cellsWithouWolfs.Length)];
			this.MoveTo(cellToGo);
		}

		private void AddCloneToEmptyCell(int radius)
		{
			Coordinates2D[] emptyCells = GetEmptySuburbCells(radius);
			if(emptyCells.IsEmpty())
				return;
			Coordinates2D cellToAdd = emptyCells[Random.Next(emptyCells.Length)];
			this.AddTo(new PreyAnt(World, Weight / 2), cellToAdd);
		}
	}
}