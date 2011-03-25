using System.Collections.Generic;
using SILibrary.General;

namespace SILibrary.TwoDimensional
{
    public class FourConnectedSurfaceTopology: BiConnectedTopologyBase<Coordinates2D>
    {
        private readonly Boundaries2D boundaries;

        public FourConnectedSurfaceTopology(Boundaries2D boundaries): base(boundaries)
        {
            this.boundaries = boundaries;
        }

        protected override IEnumerable<Coordinates2D> GetNeighbours(Coordinates2D coord)
        {
            if(coord.x > boundaries.TopLeft.x)
                yield return new Coordinates2D(coord.x - 1, coord.y);
            if(coord.x < boundaries.BottomRight.x)
                yield return new Coordinates2D(coord.x + 1, coord.y);
            if(coord.y > boundaries.TopLeft.y)
                yield return new Coordinates2D(coord.x, coord.y - 1);
            if(coord.y < boundaries.BottomRight.y)
                yield return new Coordinates2D(coord.x, coord.y + 1);
        }
    }
}