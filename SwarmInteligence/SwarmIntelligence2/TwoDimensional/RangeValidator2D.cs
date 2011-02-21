using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.TwoDimensional
{
    public class RangeValidator2D: RangeValidator<Coordinates2D>
    {
        public static void Register()
        {
            Register(new RangeValidator2D());
        }

        #region Overrides of RangeValidator<Coordinates2D>

        public override int Compare(Coordinates2D a, Coordinates2D b)
        {
            return a.x == b.x
                       ? a.y.CompareTo(b.y)
                       : a.x.CompareTo(b.x);
        }

        #endregion
    }
}