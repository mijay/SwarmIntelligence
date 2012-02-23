using System;
using Common;
using SILibrary.Common;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation;
using SwarmIntelligence.Implementation.MemoryManagement;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.BuildUp
{
	internal class MapConfiguration<TCoordinate, TNodeData, TEdgeData>: DisposableBase,
	                                                                    SystemBuilder.IMapConfiguration<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld;

		public MapConfiguration(BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld)
		{
			this.buildingWorld = buildingWorld;
		}

		#region Implementation of IMapConfiguration<TCoordinate,TNodeData,TEdgeData>

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithMap(
			SystemBuilder.MapBuilder<TCoordinate, TNodeData, TEdgeData> mapBuilder)
		{
			CheckDisposedState();
			buildingWorld.Map = mapBuilder(buildingWorld.Topology);
			Dispose();
			return new DataLayersConfiguration<TCoordinate, TNodeData, TEdgeData>(buildingWorld);
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap(
			MappingBuilder<TCoordinate, TNodeData, TEdgeData> mappingBuilder)
		{
			return WithMap(topology => new Map<TCoordinate, TNodeData, TEdgeData>(topology, mappingBuilder));
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap<TMapping>(
			SystemBuilder.CellProviderBuilder<TCoordinate, TNodeData, TEdgeData> cellProviderBuilder)
			where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		{
			return WithDefaultMap(EntityBuilders<TCoordinate, TNodeData, TEdgeData>.ForMapping<TMapping>(
				cellProviderBuilder, buildingWorld.Topology, buildingWorld.Log));
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMapCellProvider<TMapping>(
			CellBuilder<TCoordinate, TNodeData, TEdgeData> cellBuilder)
			where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		{
			return WithDefaultMap<TMapping>(map => new CellProvider<TCoordinate, TNodeData, TEdgeData>(map, cellBuilder));
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMapCellProvider<TMapping, TCell>()
			where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
			where TCell: CellBase<TCoordinate, TNodeData, TEdgeData>
		{
			return WithDefaultMapCellProvider<TMapping>(EntityBuilders<TCoordinate, TNodeData, TEdgeData>.ForCell<TCell>());
		}

		

		#endregion
	}
}