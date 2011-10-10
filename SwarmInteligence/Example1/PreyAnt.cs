using System;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.Common;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace Example1
{
    class PreyAnt : AntBase<Coordinates2D, EmptyData, EmptyData>
    {
        private const int Speed = 2;

        public PreyAnt(World<Coordinates2D, EmptyData, EmptyData> world) : base(world)
        {
        }

        public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
        {
            var isAntExists = false;

            var cells = CellsBrowser.GetCellsWithRadius(outlook, Speed, new Coordinates2D(0, 0), new Coordinates2D(10, 10));
            foreach (var cell in cells)
            {
                if (!cell.OfType<WolfAnt>().IsNotEmpty())
                {
                    this.MoveTo(cell.Coordinate);
                    isAntExists = true;
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
