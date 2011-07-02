using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using SwarmIntelligence.Core.Data;
using Utils.Drawing;

namespace SILibrary.TwoDimensional
{
    public class PictureNodeDataLayer: NodeDataLayer<Coordinates2D, Color>, IDisposable
    {
        private readonly FastBitmap data;
        private bool disposed;

        public PictureNodeDataLayer(Bitmap data, SurfaceTopology topology)
            : base(topology)
        {
            Contract.Requires(data != null && topology != null);
            Contract.Requires(topology.TopLeft.Equals(new Coordinates2D(0, 0))
                              && topology.BottomRight.Equals(new Coordinates2D(data.Width - 1, data.Height - 1)));

            this.data = new FastBitmap(data);
        }

        #region Overrides of NodeDataLayer<Coordinates2D,Color>

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

        ~PictureNodeDataLayer()
        {
            Dispose();
        }

        #endregion
    }
}