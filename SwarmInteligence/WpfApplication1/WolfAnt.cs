using System;
using System.Collections.Generic;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace WpfApplication1
{
	internal class WolfAnt: Animal
	{
		private static readonly Random random = new Random();
		private const int Speed = 3;
	    private const double MassPrey = 0.5;

		public WolfAnt(World<Coordinates2D, EmptyData, EmptyData> world, double weight)
			: base(world, weight)
		{
		}

		public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
		{
			ICell<Coordinates2D, EmptyData, EmptyData>[] cellsWithPreys = outlook.Cell
				.GetSuburbCells(Speed)
				.Where(cell => cell.OfType<PreyAnt>().IsNotEmpty())
				.ToArray();

		    Coordinates2D newCoordinate;
		    new Coordinates2D();

		    if(cellsWithPreys.IsNotEmpty()) {
				ICell<Coordinates2D, EmptyData, EmptyData> target = cellsWithPreys.First();
                var newWeight = target.OfType<PreyAnt>().First().weight * MassPrey + weight;
                if (newWeight > 6)
                {
                    newWeight = 6;
                }
			    weight = newWeight;
			    newCoordinate = target.Coordinate;
				this.RemoveFrom(target.Coordinate, target.OfType<PreyAnt>().First());
			} else {
				Coordinates2D[] allCells = outlook.Cell
					.GetSuburb(Speed)
					.ToArray();

			    weight--;
				Coordinates2D cellToGo = allCells[random.Next(allCells.Length)];
			    newCoordinate = cellToGo;
			}

            if (weight == 6)
            {
                var emptyCells = GetEmptySuborbCells(outlook, reproductionRadius);
                Reproducing(outlook.World, weight, true, emptyCells[random.Next(emptyCells.Length)]);

            } else if (weight == 0)
            {
                this.RemoveFrom(outlook.Coordinate, this);
            } else
            {
                this.MoveTo(newCoordinate);
            }
		}
	}
}