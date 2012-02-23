using SILibrary.Common;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.TwoDimensional
{
	public class SurfaceMapping<TValue>: ListBasedMappingBase<Coordinates2D, TValue>
		where TValue: class
	{
		private readonly int maxX;
		private readonly int minX;
		private readonly int minY;

		public SurfaceMapping(IValueProvider<Coordinates2D, TValue> valueProvider, SurfaceTopology surfaceTopology, ILog log)
			: base(valueProvider, log)
		{
			minX = surfaceTopology.TopLeft.x;
			minY = surfaceTopology.TopLeft.y;
			maxX = surfaceTopology.BottomRight.x;
		}

		#region Overrides of ListBasedMappingBase<Coordinates2D,TValue>

		protected override int ToIndex(Coordinates2D key)
		{
			return (key.y - minY) * (maxX - minX) + (key.x - minX);
		}

		protected override Coordinates2D ToKey(int index)
		{
			return new Coordinates2D(index % (maxX - minX), index / (maxX - minX));
		}

		#endregion
	}
}