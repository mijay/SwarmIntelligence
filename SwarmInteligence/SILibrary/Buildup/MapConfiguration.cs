using System;
using Common;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation;
using SwarmIntelligence.Implementation.MemoryManagement;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.Buildup
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

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithMap(Map<TCoordinate, TNodeData, TEdgeData> map)
		{
			CheckDisposedState();
			buildingWorld.Map = map;
			Dispose();
			return new DataLayersConfiguration<TCoordinate, TNodeData, TEdgeData>(buildingWorld);
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithMapCreatedBy(
			Func<Topology<TCoordinate>, Map<TCoordinate, TNodeData, TEdgeData>> mapBuilder)
		{
			return WithMap(mapBuilder(buildingWorld.Topology));
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap(
			MappingBuilder<TCoordinate, TNodeData, TEdgeData> mappingBuilder)
		{
			return WithMapCreatedBy(topology => new Map<TCoordinate, TNodeData, TEdgeData>(topology, mappingBuilder));
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap<TMapping>(
			CellProviderBuilder<TCoordinate, TNodeData, TEdgeData> cellProviderBuilder)
			where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		{
			return WithMap(
				new Map<TCoordinate, TNodeData, TEdgeData>(
					buildingWorld.Topology,
					map => (TMapping) Activator.CreateInstance(typeof(TMapping), cellProviderBuilder(map), buildingWorld.Log)));
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap<TMapping>(
			CellBuilder<TCoordinate, TNodeData, TEdgeData> cellBuilder)
			where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		{
			return WithDefaultMap<TMapping>(map => new CellProvider<TCoordinate, TNodeData, TEdgeData>(map, cellBuilder));
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap<TMapping, TCell>()
			where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
			where TCell: CellBase<TCoordinate, TNodeData, TEdgeData>
		{
			return WithDefaultMap<TMapping>(EntityBuilders<TCoordinate, TNodeData, TEdgeData>.ForCell<TCell>());
		}

		#endregion
	}
}