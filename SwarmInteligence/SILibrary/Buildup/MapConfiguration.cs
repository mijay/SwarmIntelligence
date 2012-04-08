using Common;
using SwarmIntelligence.Core.Space;
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
			ValueProviderBuilder<TCoordinate, TNodeData, TEdgeData> valueProviderBuilder,
			SystemBuilder.ValueStorageBuilder<TCoordinate, TNodeData, TEdgeData> valueStorageBuilder)
		{
			return WithMap(topology => new Map<TCoordinate, TNodeData, TEdgeData>(topology, valueProviderBuilder, valueStorageBuilder(topology)));
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMap<TValueStorage>(
			ValueProviderBuilder<TCoordinate, TNodeData, TEdgeData> cellProviderBuilder)
			where TValueStorage: IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		{
			return WithDefaultMap(cellProviderBuilder,
				EntityBuilders<TCoordinate, TNodeData, TEdgeData>.ForStorage<TValueStorage>);
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMapCellProvider<TValueStorage>(
			CellBuilder<TCoordinate, TNodeData, TEdgeData> cellBuilder)
			where TValueStorage: IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		{
			return WithDefaultMap<TValueStorage>(map => new CellProvider<TCoordinate, TNodeData, TEdgeData>(map, cellBuilder));
		}

		public SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultMapCellProvider<TValueStorage, TCell>()
			where TValueStorage: IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
			where TCell: CellBase<TCoordinate, TNodeData, TEdgeData>
		{
			return WithDefaultMapCellProvider<TValueStorage>(EntityBuilders<TCoordinate, TNodeData, TEdgeData>.ForCell<TCell>());
		}

		#endregion
	}
}