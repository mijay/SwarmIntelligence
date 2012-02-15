using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Implementation.Data;
using Utils.Drawing;

namespace SILibrary.TwoDimensional
{
	public static class PictureNodeDataLayer
	{
		public static NodesDataLayer<Coordinates2D, Color> Create(SurfaceTopology topology, Bitmap bitmap)
		{
			Contract.Requires(topology != null && bitmap != null);
			Contract.Requires(topology.BottomRight.x == bitmap.Width - 1);
			Contract.Requires(topology.TopLeft.x == 0);
			Contract.Requires(topology.BottomRight.y == bitmap.Height - 1);
			Contract.Requires(topology.TopLeft.y == 0);
			return new NodesDataLayer<Coordinates2D, Color>(topology, new BitmapProxy(bitmap));
		}

		#region Nested type: BitmapProxy

		private class BitmapProxy: ICompleteMapping<Coordinates2D, Color>
		{
			private readonly FastBitmap fastBitmap;
			private readonly int height;
			private readonly int width;

			public BitmapProxy(Bitmap bitmap)
			{
				fastBitmap = new FastBitmap(bitmap);
				width = bitmap.Width;
				height = bitmap.Height;
			}

			#region ICompleteMapping<Coordinates2D,Color> Members

			public Color Get(Coordinates2D key)
			{
				return fastBitmap.GetColor(key.x, key.y);
			}

			public IEnumerator<KeyValuePair<Coordinates2D, Color>> GetEnumerator()
			{
				for(int x = 0; x < width; x++)
					for(int y = 0; y < height; y++)
						yield return new KeyValuePair<Coordinates2D, Color>(new Coordinates2D(x, y), fastBitmap.GetColor(x, y));
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			#endregion
		}

		#endregion
	}
}