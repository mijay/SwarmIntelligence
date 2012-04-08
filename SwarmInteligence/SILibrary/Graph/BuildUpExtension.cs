using SILibrary.BuildUp;
using SILibrary.Common;
using SwarmIntelligence.Implementation.Playground;

namespace SILibrary.Graph
{
	public static class BuildUpExtension
	{
		public static SystemBuilder.INodeDataConfiguration<GraphCoordinate, TNodeData, TEdgeData> WithGraphMap<TNodeData, TEdgeData>(
			this SystemBuilder.IMapConfiguration<GraphCoordinate, TNodeData, TEdgeData> mapConfiguration)
		{
			return mapConfiguration.WithDefaultMapCellProvider<
				GraphValueStorage<CellBase<GraphCoordinate, TNodeData, TEdgeData>>,
				SetCell<GraphCoordinate, TNodeData, TEdgeData>>();
		}
	}
}