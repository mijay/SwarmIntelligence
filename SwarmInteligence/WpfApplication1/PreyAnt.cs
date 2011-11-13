using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Collections.Extensions;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace WpfApplication1
{

    internal class PreyAnt : Animal
    {
    	private static readonly Random random = new Random();
    	private const int Speed = 2;

        public PreyAnt(World<Coordinates2D, EmptyData, EmptyData> world, double weight)
            : base(world, weight)
        {
        }

        public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
        {
            Coordinates2D[] cellsWithWolfs = outlook.Cell
                .GetSuburbCells(Speed)
                .Where(cell => cell.OfType<WolfAnt>().IsNotEmpty())
                .Select(cell => cell.Coordinate)
                .ToArray();

            Coordinates2D[] cellsToGoInto = outlook.Cell
                .GetSuburb(Speed)
                .Except(cellsWithWolfs)
                .ToArray();

            Coordinates2D cellToGo = cellsToGoInto[random.Next(cellsToGoInto.Length)];
            if (weight == 6)
            {
                var emptyCells = GetEmptySuborbCells(outlook, reproductionRadius);
                Reproducing(outlook.World, weight, false, emptyCells[random.Next(emptyCells.Length)]);
                weight = 3;
            } else
            {
                weight++;
                if (weight > 6)
                {
                    weight = 6;
                }
            }
            this.MoveTo(cellToGo);
        }
    }
}
