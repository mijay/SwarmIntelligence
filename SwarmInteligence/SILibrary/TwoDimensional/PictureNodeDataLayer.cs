using System;
using System.Drawing;
using Common;
using SwarmIntelligence.Core;
using Utils.Drawing;

namespace SILibrary.TwoDimensional
{
	public class PictureNodeDataLayer: DataLayer<Coordinates2D, Color>, IDisposable
	{
		private readonly FastBitmap bitmap;
		private bool disposed;

		public PictureNodeDataLayer(Bitmap data)
		{
			Requires.NotNull(data);
			bitmap = new FastBitmap(data);
		}

		#region Implementation of IDisposable

		public void Dispose()
		{
			if(disposed)
				return;
			bitmap.Dispose();
			GC.SuppressFinalize(this);
			disposed = true;
		}

		~PictureNodeDataLayer()
		{
			Dispose();
		}

		#endregion

		#region Overrides of DataLayer<Coordinates2D,Color>

		public override Color Get(Coordinates2D key)
		{
			return bitmap.GetColor(key.x, key.y);
		}

		public override void Set(Coordinates2D key, Color data)
		{
			throw new NotSupportedException();
		}

		#endregion
	}
}