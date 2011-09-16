using System;
using Common.Memoization;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General.Background
{
	public class CachedEdgeDataLayer<C, E>: EdgeDataLayer<C, E>
		where C: ICoordinate<C>
	{
		private readonly Func<Edge<C>, E> cachedGet;

		public CachedEdgeDataLayer(EdgeDataLayer<C, E> edgeDataLayer, IMemoizer memoizer): base(edgeDataLayer.Topology)
		{
			cachedGet = memoizer.Memoize<Edge<C>, E>(c => edgeDataLayer[c]);
		}

		#region Overrides of EdgeDataLayer<C,E>

		public override E this[Edge<C> key]
		{
			get { return cachedGet(key); }
		}

		#endregion
	}
}