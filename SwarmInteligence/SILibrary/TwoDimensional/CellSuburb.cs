using System;
using System.Collections.Generic;
using SwarmIntelligence.Core.Playground;
using System.Linq;
using Common.Collections.Extensions;

namespace SILibrary.TwoDimensional
{
	public static class CellSuburb
	{
		public static IEnumerable<Coordinates2D> GetSuburb<TNodeData, TEdgeData>(
			this ICell<Coordinates2D, TNodeData, TEdgeData> cell, int radius)
		{
			var surfaceTopology = (SurfaceTopology) cell.Map.Topology;

			int minX = Math.Max(cell.Coordinate.x - radius, surfaceTopology.TopLeft.x);
			int maxX = Math.Min(cell.Coordinate.x + radius, surfaceTopology.BottomRight.x);

			int minY = Math.Max(cell.Coordinate.y - radius, surfaceTopology.TopLeft.y);
			int maxY = Math.Min(cell.Coordinate.y + radius, surfaceTopology.BottomRight.y);

			for(int i = minX; i <= maxX; ++i)
				for(int j = minY; j <= maxY; ++j) {
					var point = new Coordinates2D(i, j);
					if(cell.Coordinate != point)
						yield return point;
				}
		}

		public static IEnumerable<ICell<Coordinates2D, TNodeData, TEdgeData>> GetSuburbCells<TNodeData, TEdgeData>(
			this ICell<Coordinates2D, TNodeData, TEdgeData> cell, int radius)
		{
			var map = cell.Map;
			return cell
				.GetSuburb(radius)
				.Select(p => {
				        	ICell<Coordinates2D, TNodeData, TEdgeData> c;
				        	return map.TryGet(p, out c) ? c : null;
				        })
				.NotNull();
		}

	}
}