using System;
using SILibrary.Base;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.Data;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Infrastructure.Playground;

namespace SILibrary.General
{
	public static class SystemBuilder
	{
		public static ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> Create<TCoordinate, TNodeData, TEdgeData>()
			where TCoordinate: ICoordinate<TCoordinate>
		{
			var result = new SystemDeclaration<TCoordinate, TNodeData, TEdgeData>
			             {
			             	LogManager = new LogManager()
			             };
			if(typeof(TNodeData) == typeof(EmptyData))
				result.NodeDataLayerBuilder = t => new NodesDataLayer<TCoordinate, TNodeData>(t,
				                                   	(MappingBase<TCoordinate, TNodeData>) (object) new EmptyMapping<TCoordinate>());
			if(typeof(TEdgeData) == typeof(EmptyData))
				result.EdgeDataLayerBuilder = t => new EdgesDataLayer<TCoordinate, TEdgeData>(t,
				                                   	(MappingBase<Edge<TCoordinate>, TEdgeData>) (object) new EmptyMapping<Edge<TCoordinate>>());
			return result;
		}

		#region Nested type: SystemDeclaration

		private class SystemDeclaration<TCoordinate, TNodeData, TEdgeData>: ISystemDeclaration<TCoordinate, TNodeData, TEdgeData>
			where TCoordinate: ICoordinate<TCoordinate>
		{
			public Topology<TCoordinate> Topology { get; set; }
			public Func<Topology<TCoordinate>, IEdgesDataLayer<TCoordinate, TEdgeData>> EdgeDataLayerBuilder { get; set; }
			public Func<Topology<TCoordinate>, INodesDataLayer<TCoordinate, TNodeData>> NodeDataLayerBuilder { get; set; }
			public LogManager LogManager { get; set; }

			#region ISystemDeclaration<TCoordinate,TNodeData,TEdgeData> Members

			public ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithTopology(Topology<TCoordinate> topology)
			{
				Topology = topology;
				return this;
			}

			public Tuple<Runner<TCoordinate, TNodeData, TEdgeData>, ILogJournal> Build()
			{
				var map = new Map<TCoordinate, TNodeData, TEdgeData>(Topology,
					new DictionaryMapping<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>(
						TODO, LogManager.Log));
				var world = new World<TCoordinate, TNodeData, TEdgeData>(NodeDataLayerBuilder(Topology), EdgeDataLayerBuilder(Topology), map,
					LogManager.Log);
				var runner = new Runner<TCoordinate, TNodeData, TEdgeData>(world);
				return Tuple.Create(runner, LogManager.Journal);
			}

			public ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithEdgeData(
				Func<Topology<TCoordinate>, IEdgesDataLayer<TCoordinate, TEdgeData>> dataLayerBuilder)
			{
				EdgeDataLayerBuilder = dataLayerBuilder;
				return this;
			}

			public ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithNodeData(
				Func<Topology<TCoordinate>, INodesDataLayer<TCoordinate, TNodeData>> dataLayerBuilder)
			{
				NodeDataLayerBuilder = dataLayerBuilder;
				return this;
			}

			#endregion
		}

		#endregion
	}
}