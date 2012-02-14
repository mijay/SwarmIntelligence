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
	public class NodesDataLayer<TCoordinates, TNodeData>: INodesDataLayer<TCoordinates, TNodeData>
		where TCoordinates: ICoordinate<TCoordinates>
	{
		private readonly MappingBase<TCoordinates, TNodeData> mappingBase;

		public NodesDataLayer(Topology<TCoordinates> topology, MappingBase<TCoordinates, TNodeData> mappingBase)
		{
			Contract.Requires(topology != null && mappingBase != null);
			Topology = topology;
			this.mappingBase = mappingBase;
		}

		#region Implementation of INodesDataLayer<TCoordinates,TNodeData>

		public Topology<TCoordinates> Topology { get; private set; }

		public TNodeData Get(TCoordinates key)
		{
			return mappingBase.Get(key);
		}

		public bool TryGet(TCoordinates key, out TNodeData value)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(key));
			return mappingBase.TryGet(key, out value);
		}

		public IEnumerator<KeyValuePair<TCoordinates, TNodeData>> GetEnumerator()
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