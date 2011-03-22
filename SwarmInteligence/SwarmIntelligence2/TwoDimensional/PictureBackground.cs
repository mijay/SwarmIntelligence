using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using SwarmIntelligence2.Core.World.Data;
using Utils.Drawing;

namespace SwarmIntelligence2.TwoDimensional
{
    public class PictureBackground: Background<Coordinates2D, Color>, IDisposable
    {
        private readonly FastBitmap data;
        private bool disposed;

        public PictureBackground(Bitmap data)
            : base(new Boundaries2D(new Coordinates2D(0, 0),
                                    new Coordinates2D(data.Width - 1, data.Height - 1)))
        {
            Contract.Requires(data != null);
            this.data = new FastBitmap(data);
        }

        #region Overrides of Background<Coordinates2D,Color>

        public override Color this[Coordinates2D key]
        {
            get { return data.GetColor(key.x, key.y); }
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