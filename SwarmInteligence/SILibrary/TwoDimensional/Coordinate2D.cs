using SwarmIntelligence.Core.Space;

namespace SILibrary.TwoDimensional
{
	public struct Coordinates2D: ICoordinate<Coordinates2D>
	{
		public readonly int x;
		public readonly int y;

		public Coordinates2D(int x, int y)
		{
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

		public override int GetHashCode()
		{
			unchecked {
				return (x.GetHashCode() * 397) ^ y.GetHashCode();
			}
		}

		#endregion

		public override string ToString()
		{
			return string.Format("x: {0}, y: {1}", x, y);
		}

		public override bool Equals(object obj)
		{
			return obj is Coordinates2D && Equals((Coordinates2D) obj);
		}
	}
}