using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Playground;

namespace SwarmIntelligence.MemoryManagement
{
	public class CellProvider<TCoordinate, TNodeData, TEdgeData>:
		ReusingValueProviderBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly CellBuilder<TCoordinate, TNodeData, TEdgeData> builder;
		private readonly Map<TCoordinate, TNodeData, TEdgeData> map;

		public CellProvider(Map<TCoordinate, TNodeData, TEdgeData> map, CellBuilder<TCoordinate, TNodeData, TEdgeData> builder)
		{
			Contract.Requires(map != null && builder != null);
			this.map = map;
			this.builder = builder;
		}

		#region Overrides of ReusingValueProviderBase<TCoordinate,CellBase<TCoordinate,TNodeData,TEdgeData>>

		protected override CellBase<TCoordinate, TNodeData, TEdgeData> Create(TCoordinate key)
		{
			return builder(map, key);
		}

		protected override void Modify(CellBase<TCoordinate, TNodeData, TEdgeData> value, TCoordinate newKey)
		{
			value.Coordinate = newKey;
		}

		#endregion
	}
}