using System;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class CellBase<TCoordinate, TNodeData, TEdgeData>: Cell<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private TCoordinate coordinate;
		private bool initialized;

		protected CellBase(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase)
		{
			Requires.NotNull(mapBase);
			MapBase = mapBase;
		}

		public MapBase<TCoordinate, TNodeData, TEdgeData> MapBase { get; private set; }

		public abstract bool IsEmpty { get; }

		public override Map<TCoordinate, TNodeData, TEdgeData> Map
		{
			get { return MapBase; }
		}

		public override TCoordinate Coordinate
		{
			get
			{
				Requires.True<InvalidOperationException>(initialized);
				return coordinate;
			}
		}

		public abstract void Add(Ant<TCoordinate, TNodeData, TEdgeData> ant);
		public abstract void Remove(Ant<TCoordinate, TNodeData, TEdgeData> ant);

		internal void SetCoordinate(TCoordinate newCoord)
		{
			coordinate = newCoord;
			initialized = true;
		}
	}
}