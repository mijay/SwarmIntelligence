﻿using System;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Implementation.Data
{
	public class NodesDataLayer<TCoordinate, TNodeData>: INodesDataLayer<TCoordinate, TNodeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly ICompleteMapping<TCoordinate, TNodeData> completeMapping;

		public NodesDataLayer(Topology<TCoordinate> topology, ICompleteMapping<TCoordinate, TNodeData> completeMapping)
		{
			Contract.Requires(topology != null && completeMapping != null);
			Topology = topology;
			this.completeMapping = completeMapping;
		}

		#region Implementation of INodesDataLayer<TCoordinate,TNodeData>

		public Topology<TCoordinate> Topology { get; private set; }

		public TNodeData Get(TCoordinate key)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(key));
			return completeMapping.Get(key);
		}

		public void Set(TCoordinate key, TNodeData value)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(key));
			completeMapping.Set(key, value);
		}

		#endregion
	}
}