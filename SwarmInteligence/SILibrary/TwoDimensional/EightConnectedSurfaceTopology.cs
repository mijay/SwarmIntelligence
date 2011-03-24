using System.Collections.Generic;
using SILibrary.General;
using System.Linq;
using SwarmIntelligence.Utils;

namespace SILibrary.TwoDimensional
{
    public class EightConnectedSurfaceTopology: BiConnectedTopologyBase<Coordinates2D>
    {
        private readonly Boundaries2D boundaries;

        public EightConnectedSurfaceTopology(Boundaries2D boundaries): base(boundaries)
        {
            this.boundaries = boundaries;
        }

        //todo test it
        protected override IEnumerable<Coordinates2D> GetNeighbours(Coordinates2D coord)
        {
            var neighbourX = new List<int> { coord.x };
            if(coord.x < boundaries.BottomRight.x)
                neighbourX.Add(coord.x + 1);
            if(coord.x > boundaries.TopLeft.x)
                neighbourX.Add(coord.x - 1);

            var neighbourY = new List<int> { coord.y };
            if(coord.y < boundaries.BottomRight.y)
                neighbourY.Add(coord.y + 1);
            if(coord.y > boundaries.TopLeft.y)
                neighbourY.Add(coord.y - 1);

            return neighbourX
                .SetMultiply(neighbourY, (x, y) => new Coordinates2D(x, y))
                .Where(x => !x.Equals(coord))
                .ToArray();
        }
    }
}