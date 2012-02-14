using System;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.Base;
using SILibrary.TwoDimensional;
using SwarmIntelligence.Core;
using SwarmIntelligence.Infrastructure.Playground;

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

		protected Coordinates2D[] GetEmptySuburbCells(int radius)
		{
			return CellWithoutAnimalOfType<Animal>(radius);
		}

		protected Coordinates2D[] CellWithoutAnimalOfType<TAnimalType>(int radius)
			where TAnimalType: Animal
		{
			Coordinates2D[] cellsWithAnimal = Cell
				.GetSuburbCells(radius)
				.Where(cell => cell.OfType<TAnimalType>().IsNotEmpty())
				.Select(cell => cell.Coordinate)
				.ToArray();

			Coordinates2D[] cellsToGoInto = Cell
				.GetSuburb(radius)
				.Except(cellsWithAnimal)
				.ToArray();

			return cellsToGoInto;
		}
	}
}