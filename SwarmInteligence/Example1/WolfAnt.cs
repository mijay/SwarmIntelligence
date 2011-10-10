using System;
using System.Collections.Generic;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SILibrary.Common;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace Example1
{
    class WolfAnt : AntBase<Coordinates2D, EmptyData, EmptyData>
    {
        private const int Speed = 3;
        
        public WolfAnt(World<Coordinates2D, EmptyData, EmptyData> world) : base(world)
        {
        }

        public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
        {
            var isAntExists = false;

            var cells = CellsBrowser.GetCellsWithRadius(outlook, Speed, new Coordinates2D(0, 0), new Coordinates2D(10, 10));
            foreach (var cell in cells.Where(cell => cell.OfType<PreyAnt>().IsNotEmpty()))
            {
                if (cell.OfType<PreyAnt>().IsNotEmpty())
                {
                    isAntExists = true;
                    this.MoveTo(cell.Coordinate);
                    break;
                }
            }

            if (!isAntExists)
            {
                var random = new Random();
                var x = random.Next(Math.Max(outlook.Cell.Coordinate.x - Speed, 0), Math.Min(outlook.Cell.Coordinate.x + Speed, 10));
                var y = random.Next(Math.Max(outlook.Cell.Coordinate.y - Speed, 0), Math.Min(outlook.Cell.Coordinate.y + Speed, 10));
                this.MoveTo(new Coordinates2D(x, y));
            }
        }

    }
}
