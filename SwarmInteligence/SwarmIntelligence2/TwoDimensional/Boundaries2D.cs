using SwarmIntelligence2.Core.Space;

namespace SwarmIntelligence2.TwoDimensional
{
    public class Boundaries2D: Boundaries<Coordinates2D>
    {
        public Boundaries2D(Coordinates2D topLeft, Coordinates2D bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        #region Overrides of Boundaries<Coordinates2D>

        public override bool Lays(Coordinates2D coord)
        {
            return TopLeft.x <= coord.x && coord.x <= BottomRight.x &&
                   TopLeft.y <= coord.y && coord.y <= BottomRight.y;
        }

        #endregion

        public Coordinates2D TopLeft { get; private set; }
        public Coordinates2D BottomRight { get; private set; }
    }
}