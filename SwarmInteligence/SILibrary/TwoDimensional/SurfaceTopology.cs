using System.Collections.Generic;
using SwarmIntelligence.Core.Space;

namespace SILibrary.TwoDimensional
{
	public abstract class SurfaceTopology: BiConnectedTopologyBase<Coordinates2D>
	{
		protected SurfaceTopology(Coordinates2D topLeft, Coordinates2D bottomRight)
		{
			TopLeft = topLeft;
			BottomRight = bottomRight;
		}

		public Coordinates2D BottomRight { get; private set; }
		public Coordinates2D TopLeft { get; private set; }

		public override bool Lays(Coordinates2D coord)
		{
			return TopLeft.x <= coord.x && coord.x <= BottomRight.x &&
			       TopLeft.y <= coord.y && coord.y <= BottomRight.y;
		}

		public override bool Lays(Edge<Coordinates2D> edge)
		{
			return Lays(edge.from) && Lays(edge.to);
		}

		public IEnumerable<Coordinates2D> GetAllPoints()
		{
			for(int x = TopLeft.x; x <= BottomRight.x; ++x)
				for(int y = TopLeft.y; y < BottomRight.y; y++)
					yield return new Coordinates2D(x, y);
		}
	}
}