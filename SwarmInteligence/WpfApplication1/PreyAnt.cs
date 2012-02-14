using SILibrary.Base;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;

namespace WpfApplication1
{
	public class PreyAnt: Animal
	{
		public PreyAnt(World<Coordinates2D, EmptyData, EmptyData> world, int weight)
			: base(world, weight, 2)
		{
		}

		public override void ProcessTurn()
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
			Coordinates2D cellToGo = cellsWithouWolfs[Random.Next(cellsWithouWolfs.Length)];
			this.MoveTo(cellToGo);
		}

		private void AddCloneToEmptyCell(int radius)
		{
			Coordinates2D[] emptyCells = GetEmptySuburbCells(radius);
			Coordinates2D cellToAdd = emptyCells[Random.Next(emptyCells.Length)];
			this.AddTo(new PreyAnt(World, Weight / 2), cellToAdd);
		}
	}
}