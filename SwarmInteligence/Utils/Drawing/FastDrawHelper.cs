using System.Drawing;

namespace Utils.Drawing
{
	/// <summary>
	/// This class is a helper for drawing something on <see cref="FastBitmap"/>.
	/// </summary>
	public static unsafe class FastDrawHelper
	{
		/// <summary>
		/// Draw an one-pixel point of fixed color (defined by <see cref="PixelData"/>) in fixed position.
		/// </summary>
		public static void DrawPoint(FastBitmap bmp, int x, int y, PixelData color)
		{
			PixelData* addr = bmp[x, y];
			addr->R = color.R;
			addr->G = color.G;
			addr->B = color.B;
		}

		/// <summary>
		/// Draw an one-pixel point of fixed color (defined by <see cref="Color"/>) in fixed position.
		/// </summary>
		/// <remarks>
		/// If you do not use an alpha channel <see cref="Color.A"/> then
		/// it is better to use <see cref="DrawPoint(Utils.Drawing.FastBitmap,int,int,Utils.Drawing.PixelData)"/> instead.
		/// </remarks>
		public static void DrawPoint(FastBitmap bmp, int x, int y, Color color)
		{
			byte a = color.A;
			var b = (byte) ~a;
			PixelData* addr = bmp[x, y];
			checked {
				addr->R = (byte) ((addr->R * b + color.R * a) / 255);
				addr->G = (byte) ((addr->G * b + color.G * a) / 255);
				addr->B = (byte) ((addr->B * b + color.B * a) / 255);
			}
		}

		//todo: write a code!!
	}
}