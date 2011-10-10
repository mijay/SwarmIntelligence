
using System;
using System.Collections.Generic;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence.Core.Playground;

namespace SILibrary.Common
{
    public class CellsBrowser
    {
        public static List<Coordinates2D> GetCoordinatesWithRadius(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook, int radius, Coordinates2D min, Coordinates2D max)
        {
            var cell = outlook.Cell;
            var coordinates = new List<Coordinates2D>();

            for (var i = Math.Max(cell.Coordinate.x - radius, min.x); i <= Math.Min(cell.Coordinate.x + radius, max.x); ++i)
            {
                for (var j = Math.Max(cell.Coordinate.y + radius, min.y); j <= Math.Min(cell.Coordinate.y + radius, max.y); ++j)
                {
                    var point = new Coordinates2D(i, j);
                    if (cell.Coordinate.Equals(point))
                        continue;
                    coordinates.Add(point);
                }
            }

            return coordinates;
        }

        public static List<ICell<Coordinates2D, EmptyData, EmptyData>> GetCellsWithRadius(IOutlook<Coordinates2D, EmptyData, EmptyData> outlook, int radius, Coordinates2D min, Coordinates2D max)
        {
            var coordinates = GetCoordinatesWithRadius(outlook, radius, min, max);
            var cells = new List<ICell<Coordinates2D, EmptyData, EmptyData>>();
            foreach (var coordinate in coordinates)
            {
                ICell<Coordinates2D, EmptyData, EmptyData> data;
                if (outlook.Map.TryGet(coordinate, out data))
                {
                    cells.Add(data);
                }
            }

            return cells;
        }
    }
}
