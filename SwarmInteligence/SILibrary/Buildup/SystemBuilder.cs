using System;
using Common;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.MemoryManagement;
using SwarmIntelligence.Specialized;

namespace SILibrary.BuildUp
{
	public static class SystemBuilder
	{
		#region Delegates

		public delegate Map<TCoordinate, TNodeData, TEdgeData> MapBuilder<TCoordinate, TNodeData, TEdgeData>(
			Topology<TCoordinate> topology)
			where TCoordinate: ICoordinate<TCoordinate>;
		
		public delegate IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> ValueStorageBuilder<TCoordinate, TNodeData, TEdgeData>(
			Topology<TCoordinate> topology)
			where TCoordinate : ICoordinate<TCoordinate>;

		#endregion

		public static ILogConfiguration<TCoordinate, TNodeData, TEdgeData> Create<TCoordinate, TNodeData, TEdgeData>()
			where TCoordinate: ICoordinate<TCoordinate>
		{
			return new InitialConfiguration<TCoordinate, TNodeData, TEdgeData>(new BuildingWorld<TCoordinate, TNodeData, TEdgeData>());
		}

		#region Nested type: IConfigured

		public interface IConfigured<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
			where TCoordinate: ICoordinate<TCoordinate>
		{
			IConfigured<TCoordinate, TNodeData, TEdgeData> SeedData(Action<IEdgesDataLayer<TCoordinate, TEdgeData>> seed);
			IConfigured<TCoordinate, TNodeData, TEdgeData> SeedData(Action<INodesDataLayer<TCoordinate, TNodeData>> seed);
			IConfigured<TCoordinate, TNodeData, TEdgeData> SeedAnts(Action<IMapModifier<TCoordinate, TNodeData, TEdgeData>> seed);
			World<TCoordinate, TNodeData, TEdgeData> Build();
		}

		#endregion

		#region Nested type: IEdgeDataConfiguration

		public interface IEdgeDataConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
			where TCoordinate: ICoordinate<TCoordinate>
		{
			IConfigured<TCoordinate, TNodeData, TEdgeData> WithEmptyEdgeData();
			IConfigured<TCoordinate, TNodeData, TEdgeData> WithEdgeData(ICompleteMapping<Edge<TCoordinate>, TEdgeData> nodeData);
		}

		#endregion

		#region Nested type: ILogConfiguration

		public interface ILogConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
			where TCoordinate: ICoordinate<TCoordinate>
		{
			ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData> WithLog(ILog log);
			ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultLog(out ILogManager logManager);
		}

		#endregion

		#region Nested type: IMapConfiguration

		public interface IMapConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
			where TCoordinate: ICoordinate<TCoordinate>
		{
			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithMap(MapBuilder<TCoordinate, TNodeData, TEdgeData> mapBuilder);

			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap(
				ValueProviderBuilder<TCoordinate, TNodeData, TEdgeData> valueProviderBuilder,
				ValueStorageBuilder<TCoordinate, TNodeData, TEdgeData> valueStorageBuilder);

			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap<TValueStorage>(
				ValueProviderBuilder<TCoordinate, TNodeData, TEdgeData> cellProviderBuilder)
				where TValueStorage: IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>;

			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMapCellProvider<TValueStorage>(
				CellBuilder<TCoordinate, TNodeData, TEdgeData> cellBuilder)
				where TValueStorage: IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>;

			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMapCellProvider<TValueStorage, TCell>()
				where TValueStorage: IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
				where TCell: CellBase<TCoordinate, TNodeData, TEdgeData>;
		}

		#endregion

		#region Nested type: INodeDataConfiguration

		public interface INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
			where TCoordinate: ICoordinate<TCoordinate>
		{
			IEdgeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithEmptyNodeData();
			IEdgeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithNodeData(ICompleteMapping<TCoordinate, TNodeData> nodeData);
		}

		#endregion

		#region Nested type: ITopologyConfiguration

		public interface ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
			where TCoordinate: ICoordinate<TCoordinate>
		{
			IMapConfiguration<TCoordinate, TNodeData, TEdgeData> WithTopology(Topology<TCoordinate> topology);
		}

		#endregion
	}
}