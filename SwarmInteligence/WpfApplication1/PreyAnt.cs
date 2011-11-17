using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;

namespace WpfApplication1
{
	public class PreyAnt: Animal
	{
		public PreyAnt(World<Coordinates2D, EmptyData, EmptyData> world, int weight)
			: base(world, weight, 2)
		{
		}

		public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
		{
			MoveToCellWithoutWolf(outlook, speed);

			if(Weight < 6)
				Weight++;
			else {
				Weight = 3;
				AddCloneToEmptyCell(outlook, reproductionRadius);
			}
		}

		private void MoveToCellWithoutWolf(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook, int radius)
		{
			Coordinates2D[] cellsWithouWolfs = CellWithoutAnimalOfType<WolfAnt>(outlook, radius);
			Coordinates2D cellToGo = cellsWithouWolfs[Random.Next(cellsWithouWolfs.Length)];
			this.MoveTo(cellToGo);
		}

		private void AddCloneToEmptyCell(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook, int radius)
		{
			Coordinates2D[] emptyCells = GetEmptySuburbCells(outlook, radius);
			Coordinates2D cellToAdd = emptyCells[Random.Next(emptyCells.Length)];
			this.AddTo(new PreyAnt(World, Weight / 2), cellToAdd);
		}
	}
}