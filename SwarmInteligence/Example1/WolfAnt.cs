using System;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
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
            var cell = outlook.Cell;
            var isAntExists = false;

            for (var i = Math.Max(cell.Coordinate.x - Speed, 0); i <= Math.Min(cell.Coordinate.x + Speed, 10); ++i)
            {
                for (var j = Math.Max(cell.Coordinate.y - Speed, 0); j <= Math.Min(cell.Coordinate.y + Speed, 10); ++j)
                {
                    var point = new Coordinates2D(i, j);

                    if (point.Equals(cell.Coordinate))
                        continue;

                    ICell<Coordinates2D, EmptyData, EmptyData> data;
                    if (outlook.Map.TryGet(point, out data) && data.OfType<PreyAnt>().IsNotEmpty())
                    {
                        isAntExists = true;
                        this.MoveTo(point);
                    }
                }
            }

            if (!isAntExists)
            {
                var random = new Random();
                var x = random.Next(Math.Max(cell.Coordinate.x - Speed, 0), Math.Min(cell.Coordinate.x + Speed, 10));
                var y = random.Next(Math.Max(cell.Coordinate.y - Speed, 0), Math.Min(cell.Coordinate.y + Speed, 10));
                this.MoveTo(new Coordinates2D(x, y));
            }
        }

    }
}
