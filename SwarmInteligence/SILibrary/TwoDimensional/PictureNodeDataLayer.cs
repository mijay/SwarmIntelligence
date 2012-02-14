using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using Common;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;
using Utils.Drawing;

namespace SILibrary.TwoDimensional
{
	public class PictureNodeDataLayer: DisposableBase, INodesDataLayer<Coordinates2D, Color>
	{
		private readonly FastBitmap bitmap;
		private readonly SurfaceTopology topology;

		public PictureNodeDataLayer(SurfaceTopology topology, Bitmap data)
		{
			Contract.Requires(topology != null && data != null);
			Contract.Requires(topology.BottomRight.x - topology.TopLeft.x == data.Width);
			Contract.Requires(topology.BottomRight.y - topology.TopLeft.y == data.Height);
			this.topology = topology;
			bitmap = new FastBitmap(data);
		}

		#region Implementation of IDisposable

		protected override void DisposeManaged()
		{
			bitmap.Dispose();
		}

		#endregion

		#region Implementation of INodesDataLayer<Coordinates2D,Color>

		public IEnumerator<KeyValuePair<Coordinates2D, Color>> GetEnumerator()
		{
			return topology.GetAllPoints().Select(x => new KeyValuePair<Coordinates2D, Color>(x, Get(x))).GetEnumerator();
		}

		public bool TryGet(Coordinates2D key, out Color value)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(key));
			value = Get(key);
			return true;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Topology<Coordinates2D> Topology
		{
			get { return topology; }
		}

		public Color Get(Coordinates2D key)
		{
			return bitmap.GetColor(key.x, key.y);
		}

		#endregion
	}
}