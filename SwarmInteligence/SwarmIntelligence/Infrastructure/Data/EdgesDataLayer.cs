using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SwarmIntelligence.Infrastructure.Data
{
	public class EdgesDataLayer<TCoordinates, TEdgeData>: IEdgesDataLayer<TCoordinates, TEdgeData>
		where TCoordinates: ICoordinate<TCoordinates>
	{
		private readonly MappingBase<Edge<TCoordinates>, TEdgeData> mappingBase;

		public EdgesDataLayer(Topology<TCoordinates> topology, MappingBase<Edge<TCoordinates>, TEdgeData> mappingBase)
		{
			Contract.Requires(topology != null && mappingBase != null);
			Topology = topology;
			this.mappingBase = mappingBase;
		}

		#region Implementation of IEdgesDataLayer<TCoordinates,TEdgeData>

		public Topology<TCoordinates> Topology { get; private set; }

		public bool TryGet(Edge<TCoordinates> key, out TEdgeData value)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(key));
			return mappingBase.TryGet(key, out value);
		}

		public IEnumerator<KeyValuePair<Edge<TCoordinates>, TEdgeData>> GetEnumerator()
		{
			return mappingBase.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}