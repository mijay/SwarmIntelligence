using System.Collections.Generic;

namespace SILibrary.TwoDimensional
{
    public class FourConnectedSurfaceTopology: SurfaceTopology
    {
        public FourConnectedSurfaceTopology(Coordinates2D topLeft, Coordinates2D bottomRight): base(topLeft, bottomRight) {}

        protected override IEnumerable<Coordinates2D> GetNeighbours(Coordinates2D coord)
        {
            if(coord.x > TopLeft.x)
                yield return new Coordinates2D(coord.x - 1, coord.y);
            if(coord.x < BottomRight.x)
                yield return new Coordinates2D(coord.x + 1, coord.y);
            if(coord.y > TopLeft.y)
                yield return new Coordinates2D(coord.x, coord.y - 1);
            if(coord.y < BottomRight.y)
                yield return new Coordinates2D(coord.x, coord.y + 1);
        }
    }
}