using System.Collections.Generic;
using System.Linq;
using Common.Collections;

namespace SILibrary.TwoDimensional
{
	public class EightConnectedSurfaceTopology: SurfaceTopology
	{
		public EightConnectedSurfaceTopology(Coordinates2D topLeft, Coordinates2D bottomRight): base(topLeft, bottomRight) {}

		protected override IEnumerable<Coordinates2D> GetNeighbours(Coordinates2D coord)
		{
			var neighbourX = new List<int> { coord.x };
			if(coord.x < BottomRight.x)
				neighbourX.Add(coord.x + 1);
			if(coord.x > TopLeft.x)
				neighbourX.Add(coord.x - 1);

			var neighbourY = new List<int> { coord.y };
			if(coord.y < BottomRight.y)
				neighbourY.Add(coord.y + 1);
			if(coord.y > TopLeft.y)
				neighbourY.Add(coord.y - 1);

			return neighbourX
				.SetMultiply(neighbourY, (x, y) => new Coordinates2D(x, y))
				.Where(x => !x.Equals(coord))
				.ToArray();
		}
	}
}