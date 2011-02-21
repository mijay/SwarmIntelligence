using System.Diagnostics.Contracts;

namespace SwarmIntelligence2.Core.Interface
{
    /// <summary>
    /// Represents the range of coordinates.
    /// </summary>
    /// <typeparam name="C">Type of the coordinates.</typeparam>
    public struct Range<C>
        where C: struct, ICoordinate<C>
    {
        public readonly C max;
        public readonly C min;

        public Range(C min, C max)
        {
            Contract.Ensures(RangeValidator<C>.Instance.LessOrEqual(min, max));
            this.min = min;
            this.max = max;
        }
    }
}