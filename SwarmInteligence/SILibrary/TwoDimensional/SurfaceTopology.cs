using SILibrary.General;
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

        public override bool Exists(Edge<Coordinates2D> edge)
        {
            return true;
        }
    }
}