using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using SwarmIntelligence.Core.Playground;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class CellBase<TCoordinate, TNodeData, TEdgeData>: ICell<TCoordinate, TNodeData, TEdgeData>
	{
		private TCoordinate coordinate;
		private bool initialized;

		protected CellBase(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase)
		{
			Requires.NotNull(mapBase);
			MapBase = mapBase;
		}

		public MapBase<TCoordinate, TNodeData, TEdgeData> MapBase { get; private set; }

		#region ICell<TCoordinate,TNodeData,TEdgeData> Members

		public Map<TCoordinate, TNodeData, TEdgeData> Map
		{
			get { return MapBase; }
		}

		public TCoordinate Coordinate
		{
			get
			{
				Requires.True<InvalidOperationException>(initialized);
				return coordinate;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		internal void SetCoordinate(TCoordinate newCoord)
		{
			coordinate = newCoord;
			initialized = true;
		}

		#region Abstract Methods

		public abstract bool IsEmpty { get; }
		public abstract IEnumerator<IAnt<TCoordinate, TNodeData, TEdgeData>> GetEnumerator();
		public abstract void Add(IAnt<TCoordinate, TNodeData, TEdgeData> ant);
		public abstract void Remove(IAnt<TCoordinate, TNodeData, TEdgeData> ant);

		#endregion
	}
}