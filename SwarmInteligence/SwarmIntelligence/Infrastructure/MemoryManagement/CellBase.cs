using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class CellBase<TCoordinate, TNodeData, TEdgeData>: Cell<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly MapBase<TCoordinate, TNodeData, TEdgeData> map;
		private TCoordinate coordinate;

		protected CellBase(MapBase<TCoordinate, TNodeData, TEdgeData> map)
		{
			Contract.Requires(map != null);

			this.map = map;
		}

		public abstract bool IsEmpty { get; }

		public override Map<TCoordinate, TNodeData, TEdgeData> Map
		{
			get { return map; }
		}

		public override TCoordinate Coordinate
		{
			get { return coordinate; }
		}

		internal void SetCoordinate(TCoordinate newCoord)
		{
			coordinate = newCoord;
		}
	}
}