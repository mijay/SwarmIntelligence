using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class CellBase<TCoordinate, TNodeData, TEdgeData>: ICell<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private TCoordinate coordinate;
		private bool initialized;

		protected CellBase(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase)
		{
			Contract.Requires(mapBase != null);
			MapBase = mapBase;
		}

		[Pure]
		public MapBase<TCoordinate, TNodeData, TEdgeData> MapBase { get; private set; }

		#region ICell<TCoordinate,TNodeData,TEdgeData> Members

		public IMap<TCoordinate, TNodeData, TEdgeData> Map
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

		[Pure]
		public abstract bool IsEmpty { get; }

		public abstract IEnumerator<IAnt<TCoordinate, TNodeData, TEdgeData>> GetEnumerator();
		public abstract void Add(IAnt<TCoordinate, TNodeData, TEdgeData> ant);

		/// <exception cref="ArgumentOutOfRangeException"><paramref name="ant"/> does not contained in current <see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/></exception>
		public abstract void Remove(IAnt<TCoordinate, TNodeData, TEdgeData> ant);

		#endregion
	}
}