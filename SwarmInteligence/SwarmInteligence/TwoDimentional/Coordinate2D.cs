using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SwarmInteligence.TwoDimentional
{
    /// <summary>
    /// Implementation of <see cref="ICoordinate{C}"/> for 2 dimensional discrete map.
    /// </summary>
    [ContractVerification(false)]
    public struct Coordinate2D: ICoordinate<Coordinate2D>
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public readonly int X;

        /// <summary>
        /// Y coordinate
        /// </summary>
        public readonly int Y;

        public Coordinate2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate2D(Coordinate2D source)
        {
            X = source.X;
            Y = source.Y;
        }

        #region Implementation of ICloneable

        /// <inheritdoc/>
        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region Implementation of IEquatable<ICoordinate<Coordinate2D>>

        /// <inheritdoc/>
        public bool Equals(Coordinate2D other)
        {
            return X == other.X && Y == other.Y;
        }

        #endregion

        #region Implementation of ICoordinate<Coordinate2D>

        /// <inheritdoc/>
        public Coordinate2D Cast
        {
            get { return this; }
        }

        /// <inheritdoc/>
        public IEnumerable<Coordinate2D> Suburb(int radius)
        {
            int fromX = X - radius, fromY = Y - radius;
            int toX = X + radius, toY = Y + radius;

            for(int x = fromX; x <= toX; x++)
                for(int y = fromY; y <= toY; y++)
                    yield return new Coordinate2D(x, y);
        }

        /// <inheritdoc/>
        public IEnumerable<Coordinate2D> Range(Coordinate2D upperBound)
        {
            for(int x = X; x <= upperBound.X; x++)
                for(int y = Y; y <= upperBound.Y; y++)
                    yield return new Coordinate2D(x, y);
        }

        /// <inheritdoc/>
        public bool IsInRange(Coordinate2D lowerBound, Coordinate2D upperBound)
        {
            return X >= lowerBound.X && X <= upperBound.X && Y >= lowerBound.Y && Y <= upperBound.Y;
        }

        #endregion
    }
}