using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.Data
{
	public class NodesDataLayer<TCoordinates, TNodeData>: INodesDataLayer<TCoordinates, TNodeData>
		where TCoordinates: ICoordinate<TCoordinates>
	{
		private readonly ICompleteMapping<TCoordinates, TNodeData> completeMapping;

		public NodesDataLayer(Topology<TCoordinates> topology, ICompleteMapping<TCoordinates, TNodeData> completeMapping)
		{
			Contract.Requires(topology != null && completeMapping != null);
			Topology = topology;
			this.completeMapping = completeMapping;
		}

		#region Implementation of INodesDataLayer<TCoordinates,TNodeData>

		public Topology<TCoordinates> Topology { get; private set; }

		public TNodeData Get(TCoordinates key)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(key));
			return completeMapping.Get(key);
		}

		public IEnumerator<KeyValuePair<TCoordinates, TNodeData>> GetEnumerator()
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