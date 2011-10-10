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
				return (x * 397) ^ y;
			}
		}

		#endregion

		public override string ToString()
		{
			return string.Format("x: {0}, y: {1}", x, y);
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj))
				return false;
			if(obj.GetType() != typeof(Coordinates2D))
				return false;
			return Equals((Coordinates2D) obj);
		}

		public static bool operator ==(Coordinates2D left, Coordinates2D right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Coordinates2D left, Coordinates2D right)
		{
			return !left.Equals(right);
		}
	}
}