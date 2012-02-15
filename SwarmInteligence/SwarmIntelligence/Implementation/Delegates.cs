using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.MemoryManagement;

namespace SwarmIntelligence.Implementation
{
	public delegate CellBase<TCoordinate, TNodeData, TEdgeData> CellBuilder<TCoordinate, TNodeData, TEdgeData>(
		Map<TCoordinate, TNodeData, TEdgeData> map, TCoordinate coordinate) where TCoordinate: ICoordinate<TCoordinate>;

	public delegate IValueProvider<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> CellProviderBuilder
		<TCoordinate, TNodeData, TEdgeData>(Map<TCoordinate, TNodeData, TEdgeData> map)
		where TCoordinate: ICoordinate<TCoordinate>;

	public delegate MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> MappingBuilder<TCoordinate, TNodeData, TEdgeData>(
		Map<TCoordinate, TNodeData, TEdgeData> map) where TCoordinate: ICoordinate<TCoordinate>;
}