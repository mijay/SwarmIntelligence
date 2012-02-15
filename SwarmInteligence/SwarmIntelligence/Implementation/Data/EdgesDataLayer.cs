using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Implementation.Data
{
	public class EdgesDataLayer<TCoordinates, TEdgeData>: IEdgesDataLayer<TCoordinates, TEdgeData>
		where TCoordinates: ICoordinate<TCoordinates>
	{
		private readonly ICompleteMapping<Edge<TCoordinates>, TEdgeData> completeMapping;

		public EdgesDataLayer(Topology<TCoordinates> topology, ICompleteMapping<Edge<TCoordinates>, TEdgeData> completeMapping)
		{
			Contract.Requires(topology != null && completeMapping != null);
			Topology = topology;
			this.completeMapping = completeMapping;
		}

		#region Implementation of IEdgesDataLayer<TCoordinates,TEdgeData>

		public Topology<TCoordinates> Topology { get; private set; }

		public TEdgeData Get(Edge<TCoordinates> key)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(key));
			return completeMapping.Get(key);
		}

		public IEnumerator<KeyValuePair<Edge<TCoordinates>, TEdgeData>> GetEnumerator()
		{
			return completeMapping.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}