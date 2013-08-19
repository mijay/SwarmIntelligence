using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Playground;

namespace SwarmIntelligence.MemoryManagement
{
	public delegate CellBase<TCoordinate, TNodeData, TEdgeData> CellBuilder<TCoordinate, TNodeData, TEdgeData>(
		Map<TCoordinate, TNodeData, TEdgeData> map, TCoordinate coordinate) where TCoordinate: ICoordinate<TCoordinate>;

	public delegate IValueProvider<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> ValueProviderBuilder<TCoordinate, TNodeData, TEdgeData>(
		Map<TCoordinate, TNodeData, TEdgeData> map) where TCoordinate: ICoordinate<TCoordinate>;
}