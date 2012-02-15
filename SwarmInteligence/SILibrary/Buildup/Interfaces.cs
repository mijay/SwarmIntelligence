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
	public interface ILogConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
		where TCoordinate: ICoordinate<TCoordinate>
	{
		ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData> WithLog(ILog log);
		ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultLog(out ILogManager logManager);
	}

	public interface ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
		where TCoordinate: ICoordinate<TCoordinate>
	{
		IMapConfiguration<TCoordinate, TNodeData, TEdgeData> WithTopology(Topology<TCoordinate> topology);
	}

	public interface IMapConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
		where TCoordinate: ICoordinate<TCoordinate>
	{
		INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithMap(Map<TCoordinate, TNodeData, TEdgeData> map);
		INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithMapCreatedBy(Func<Topology<TCoordinate>, Map<TCoordinate, TNodeData, TEdgeData>> mapBuilder);
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

	public interface INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
		where TCoordinate: ICoordinate<TCoordinate>
	{
		IEdgeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithEmptyNodeData();
		IEdgeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithNodeData(ICompleteMapping<TCoordinate, TNodeData> nodeData);
	}

	public interface IEdgeDataConfiguration<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
		where TCoordinate: ICoordinate<TCoordinate>
	{
		IConfigured<TCoordinate, TNodeData, TEdgeData> WithEmptyEdgeData();
		IConfigured<TCoordinate, TNodeData, TEdgeData> WithEdgeData(ICompleteMapping<Edge<TCoordinate>, TEdgeData> nodeData);
	}

	public interface IConfigured<TCoordinate, TNodeData, TEdgeData>: IFluentInterface
		where TCoordinate: ICoordinate<TCoordinate>
	{
		IConfigured<TCoordinate, TNodeData, TEdgeData> SeedData(Action<IEdgesDataLayer<TCoordinate, TEdgeData>> seed);
		IConfigured<TCoordinate, TNodeData, TEdgeData> SeedData(Action<INodesDataLayer<TCoordinate, TNodeData>> seed);
		IConfigured<TCoordinate, TNodeData, TEdgeData> SeedAnts(Action<IMapModifier<TCoordinate, TNodeData, TEdgeData>> seed);
		World<TCoordinate, TNodeData, TEdgeData> Build();
	}
}