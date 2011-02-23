using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using SwarmIntelligence2.Core;
using SwarmIntelligence2.Core.Coordinates;
using Utils.Drawing;

namespace SwarmIntelligence2.TwoDimensional
{
    public class PictureBackground: Background<Coordinates2D, Color>, IDisposable
    {
        private readonly FastBitmap data;
        private bool disposed;

        public PictureBackground(Bitmap data)
            : base(new Range<Coordinates2D>(new Coordinates2D(0, 0),
                                            new Coordinates2D(data.Width - 1, data.Height - 1)))
        {
            Contract.Requires<ArgumentNullException>(data != null);
            this.data = new FastBitmap(data);
        }

        #region Overrides of Background<Coordinates2D,Color>

        public override Color this[Coordinates2D coord]
        {
            get { return data.GetColor(coord.x, coord.y); }
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if(disposed)
                return;
            data.Dispose();
            GC.SuppressFinalize(this);
            disposed = true;
        }

        ~PictureBackground()
        {
            Dispose();
        }

        #endregion
    }
}