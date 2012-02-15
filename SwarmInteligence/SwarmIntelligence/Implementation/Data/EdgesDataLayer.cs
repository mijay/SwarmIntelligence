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
	public class EdgesDataLayer<TCoordinate, TEdgeData>: IEdgesDataLayer<TCoordinate, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly ICompleteMapping<Edge<TCoordinate>, TEdgeData> completeMapping;

		public EdgesDataLayer(Topology<TCoordinate> topology, ICompleteMapping<Edge<TCoordinate>, TEdgeData> completeMapping)
		{
			Contract.Requires(topology != null && completeMapping != null);
			Topology = topology;
			this.completeMapping = completeMapping;
		}

		#region Implementation of IEdgesDataLayer<TCoordinate,TEdgeData>

		public Topology<TCoordinate> Topology { get; private set; }

		public TEdgeData Get(Edge<TCoordinate> key)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(key));
			return completeMapping.Get(key);
		}

		public IEnumerator<KeyValuePair<Edge<TCoordinate>, TEdgeData>> GetEnumerator()
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