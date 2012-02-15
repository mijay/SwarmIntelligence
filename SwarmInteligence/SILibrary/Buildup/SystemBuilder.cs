using System;
using Common;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.MemoryManagement;
using SwarmIntelligence.Specialized;

namespace SILibrary.Buildup
{
	public static class SystemBuilder
	{
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
			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithCommonMap();
			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithMap(Map<TCoordinate, TNodeData, TEdgeData> map);

			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithMapCreatedBy(
				Func<Topology<TCoordinate>, Map<TCoordinate, TNodeData, TEdgeData>> mapBuilder);

			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap(MappingBuilder<TCoordinate, TNodeData, TEdgeData> mappingBuilder);

			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap<TMapping>(
				CellProviderBuilder<TCoordinate, TNodeData, TEdgeData> cellProviderBuilder)
				where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>;

			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap<TMapping>(
				CellBuilder<TCoordinate, TNodeData, TEdgeData> cellBuilder)
				where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>;

			INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap<TMapping, TCell>()
				where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
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