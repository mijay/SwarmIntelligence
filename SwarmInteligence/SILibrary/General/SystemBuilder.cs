using System;
using SILibrary.Base;
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
			return new SystemDeclaration<TCoordinate, TNodeData, TEdgeData>
			       {
			       	GarbageCollector = new GarbageCollector<TCoordinate, TNodeData, TEdgeData>(),
			       	CellProvider = SetCell<TCoordinate, TNodeData, TEdgeData>.Provider(),
			       	MapType = typeof(DictionaryMap<TCoordinate, TNodeData, TEdgeData>),
			       	LogManager = new LogManager(),
			       	EdgeDataLayer = GetDataLayer<Edge<TCoordinate>, TEdgeData>(),
			       	NodeDataLayer = GetDataLayer<TCoordinate, TNodeData>()
			       };
		}

		private static DataLayer<TCoordinate, TData> GetDataLayer<TCoordinate, TData>()
		{
			if(typeof(TData) == typeof(EmptyData))
				return (DataLayer<TCoordinate, TData>) ((object) new EmptyDataLayer<TCoordinate>());
			return new DictionaryDataLayer<TCoordinate, TData>();
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

			public Tuple<Runner<TCoordinate, TNodeData, TEdgeData>, ILogJournal> Build()
			{
				var map = (IMap<TCoordinate, TNodeData, TEdgeData>) Activator.CreateInstance(MapType, Topology, CellProvider, LogManager.Log);
				var world = new World<TCoordinate, TNodeData, TEdgeData>(NodeDataLayer, EdgeDataLayer, map, LogManager.Log);
				var runner = new Runner<TCoordinate, TNodeData, TEdgeData>(world, GarbageCollector);
				return Tuple.Create(runner, LogManager.Journal);
			}

			#endregion
		}

		#endregion
	}
}