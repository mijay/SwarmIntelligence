using System;
using Common;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace Example1
{
    class WolfPreyAnt : AntBase<Coordinates2D, EmptyData, EmptyData>
    {
        private readonly bool _wolf;
        private const int WolfSpeed = 3;
        private const int PreySpeed = 2;

        public WolfPreyAnt(World<Coordinates2D, EmptyData, EmptyData> world, bool wolf)
            : base(world)
        {
            Requires.NotNull(wolf);
            _wolf = wolf;
        }

        public override void ProcessTurn(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook)
        {
            var cell = outlook.Cell;

            int i;
            int j;
            if (_wolf)
            {
                var mini = cell.Coordinate.x - WolfSpeed >= 0 ? cell.Coordinate.x - WolfSpeed : 0;
                var maxi = cell.Coordinate.x + WolfSpeed <= 10 ? cell.Coordinate.x + WolfSpeed : 10;
                var minj = cell.Coordinate.y - WolfSpeed >= 0 ? cell.Coordinate.x - WolfSpeed : 0;
                var maxj = cell.Coordinate.y + WolfSpeed <= 10 ? cell.Coordinate.y + WolfSpeed : 10;
                for (i = mini; i <= maxi; i++)
                {
                    for (j = minj; j <= maxj; j++)
                    {
                        ICell<Coordinates2D, EmptyData, EmptyData> data;
                        if (cell.Coordinate.x == i || cell.Coordinate.y == j ||
                            (!outlook.Map.TryGet(new Coordinates2D(i, j), out data))) continue;
                        this.MoveTo(new Coordinates2D(i, j));
                        return;
                    }
                }

            }
            else
            {
                var mini = cell.Coordinate.x - PreySpeed >= 0 ? cell.Coordinate.x - PreySpeed : 0;
                var maxi = cell.Coordinate.x + PreySpeed <= 10 ? cell.Coordinate.x + PreySpeed : 10;
                var minj = cell.Coordinate.y - PreySpeed >= 0 ? cell.Coordinate.x - PreySpeed : 0;
                var maxj = cell.Coordinate.y + PreySpeed <= 10 ? cell.Coordinate.y + PreySpeed : 10;
                for (i = mini; i <= maxi; i++)
                {
                    for (j = minj; j <= maxj; j++)
                    {
                        ICell<Coordinates2D, EmptyData, EmptyData> data;
                        if (cell.Coordinate.x == i || cell.Coordinate.y == j ||
                            (outlook.Map.TryGet(new Coordinates2D(i, j), out data))) continue;
                        this.MoveTo(new Coordinates2D(i, j));
                        return;
                    }
                }

            }

            var random = new Random();
            do
            {
                i = random.Next(cell.Coordinate.x - PreySpeed, cell.Coordinate.x + PreySpeed);
                j = random.Next(cell.Coordinate.y - PreySpeed, cell.Coordinate.y + PreySpeed);
            } while (i <= 10 && i >= -10 && j <= 10 && j >= -10);
            this.MoveTo(new Coordinates2D(i, j));
        }
    }
}
