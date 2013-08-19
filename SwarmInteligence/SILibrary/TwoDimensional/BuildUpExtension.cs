using SILibrary.BuildUp;
using SILibrary.Common;
using SwarmIntelligence.Implementation.Playground;

namespace SILibrary.TwoDimensional
{
	public static class BuildUpExtension
	{
		public static SystemBuilder.INodeDataConfiguration<Coordinates2D, TNodeData, TEdgeData> WithSurfaceMap<TNodeData, TEdgeData>(
			this SystemBuilder.IMapConfiguration<Coordinates2D, TNodeData, TEdgeData> mapConfiguration)
		{
			return mapConfiguration.WithDefaultMapCellProvider<
				SurfaceValueStorage<CellBase<Coordinates2D, TNodeData, TEdgeData>>,
				SetCell<Coordinates2D, TNodeData, TEdgeData>>();
		}
	}
}