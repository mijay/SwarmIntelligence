using System;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace WpfApplication1
{
	public abstract class Animal: AntBase<Coordinates2D, EmptyData, EmptyData>
	{
		protected static readonly Random Random = new Random();
		protected readonly int reproductionRadius;
		protected readonly int speed;

		protected Animal(World<Coordinates2D, EmptyData, EmptyData> world, int weight, int speed)
			: base(world)
		{
			this.speed = speed;
			Weight = weight;
			reproductionRadius = 1;
		}

		public int Weight { get; protected set; }

		protected static Coordinates2D[] GetEmptySuburbCells(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook, int radius)
		{
			return CellWithoutAnimalOfType<Animal>(outlook, radius);
		}

		protected static Coordinates2D[] CellWithoutAnimalOfType<TAnimalType>(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook, int radius)
			where TAnimalType: Animal
		{
			Coordinates2D[] cellsWithAnimal = outlook.Cell
				.GetSuburbCells(radius)
				.Where(cell => cell.OfType<TAnimalType>().IsNotEmpty())
				.Select(cell => cell.Coordinate)
				.ToArray();

			Coordinates2D[] cellsToGoInto = outlook.Cell
				.GetSuburb(radius)
				.Except(cellsWithAnimal)
				.ToArray();

			return cellsToGoInto;
		}
	}
}