using SILibrary.Common;

namespace SILibrary.TwoDimensional
{
	public class SurfaceValueStorage<TValue>: ListValueStorageBase<Coordinates2D, TValue>
		where TValue: class
	{
		private readonly int maxX;
		private readonly int minX;
		private readonly int minY;

		public SurfaceValueStorage(SurfaceTopology surfaceTopology)
		{
			minX = surfaceTopology.TopLeft.x;
			minY = surfaceTopology.TopLeft.y;
			maxX = surfaceTopology.BottomRight.x;
		}

		#region Overrides of ListValueStorageBase<Coordinates2D,TValue>

		protected override int ToIndex(Coordinates2D key)
		{
			return (key.y - minY) * (maxX - minX + 1) + (key.x - minX);
		}

		protected override Coordinates2D ToKey(int index)
		{
			return new Coordinates2D(index % (maxX - minX + 1) + minX, index / (maxX - minX + 1) + minY);
		}

		#endregion
	}
}