using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.TwoDimensional
{
    public struct Coordinates2D: ICoordinate<Coordinates2D>
    {
        public readonly int x;
        public readonly int y;

        public Coordinates2D(int x, int y)
        {
            Contract.Requires<ArgumentOutOfRangeException>(x >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(y >= 0);
            this.x = x;
            this.y = y;
        }

        #region ICoordinate<Coordinates2D> Members

        public Coordinates2D Clone()
        {
            return new Coordinates2D(x, y);
        }

        public bool Equals(Coordinates2D other)
        {
            return other.x == x && other.y == y;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        public override bool Equals(object obj)
        {
            return obj is Coordinates2D && Equals((Coordinates2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }
    }
}