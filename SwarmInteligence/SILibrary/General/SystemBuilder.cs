using System;
using SILibrary.Common;
using SILibrary.General.Background;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.GrabgeCollection;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.General
{
	public static class SystemBuilder
	{
		public static ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> Create<TCoordinate, TNodeData, TEdgeData>()
		{
			var result = new SystemDeclaration<TCoordinate, TNodeData, TEdgeData>
			             {
			             	GarbageCollector = new GarbageCollector<TCoordinate, TNodeData, TEdgeData>(),
			             	CellProvider = SetCell<TCoordinate, TNodeData, TEdgeData>.Provider(),
			             	MapType = typeof(DictionaryMap<TCoordinate, TNodeData, TEdgeData>),
							LogManager = new LogManager()
			             };
			if(typeof(TEdgeData) == typeof(EmptyData))
				result.EdgeDataLayer = (DataLayer<Edge<TCoordinate>, TEdgeData>) ((object) new EmptyDataLayer<Edge<TCoordinate>>());
			if(typeof(TNodeData) == typeof(EmptyData))
				result.NodeDataLayer = (DataLayer<TCoordinate, TNodeData>) ((object) new EmptyDataLayer<TCoordinate>());
			return result;
		}

		#region Nested type: SystemDeclaration

		private class SystemDeclaration<TCoordinate, TNodeData, TEdgeData>: ISystemDeclaration<TCoordinate, TNodeData, TEdgeData>
		{
			public Topology<TCoordinate> Topology { get; set; }
			public DataLayer<Edge<TCoordinate>, TEdgeData> EdgeDataLayer { get; set; }
			public DataLayer<TCoordinate, TNodeData> NodeDataLayer { get; set; }
			public IGarbageCollector<TCoordinate, TNodeData, TEdgeData> GarbageCollector { get; set; }
			public ICellProvider<TCoordinate, TNodeData, TEdgeData> CellProvider { get; set; }
			public Type MapType { get; set; }
			public LogManager LogManager { get; set; }

			#region ISystemDeclaration<TCoordinate,TNodeData,TEdgeData> Members

			public ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithTopology(Topology<TCoordinate> topology)
			{
				Topology = topology;
				return this;
			}

			public ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithEdgeData(DataLayer<Edge<TCoordinate>, TEdgeData> dataLayer)
			{
				EdgeDataLayer = dataLayer;
				return this;
			}

			public ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithNodeData(DataLayer<TCoordinate, TNodeData> dataLayer)
			{
				NodeDataLayer = dataLayer;
				return this;
			}

			public Runner<TCoordinate, TNodeData, TEdgeData> Build(out ILogJournal logJournal)
			{
				logJournal = LogManager.Journal;
				var map = (IMap<TCoordinate, TNodeData, TEdgeData>) Activator.CreateInstance(MapType, Topology, CellProvider, LogManager.Log);
				var world = new World<TCoordinate, TNodeData, TEdgeData>(NodeDataLayer,EdgeDataLayer, map, LogManager.Log);
				return new Runner<TCoordinate, TNodeData, TEdgeData>(world, GarbageCollector);
			}

			#endregion
		}

		#endregion
	}
}