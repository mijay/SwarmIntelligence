using System;
using System.Collections;
using System.Collections.Generic;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Implementation.Playground
{
	public abstract class CellBase<TCoordinate, TNodeData, TEdgeData>: ICell<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		protected CellBase(Map<TCoordinate, TNodeData, TEdgeData> map, TCoordinate coordinate)
		{
			Coordinate = coordinate;
			Map = map;
		}

		internal Map<TCoordinate, TNodeData, TEdgeData> Map { get; private set; }

		#region ICell<TCoordinate,TNodeData,TEdgeData> Members

		public TCoordinate Coordinate { get; internal set; }

		IMap<TCoordinate, TNodeData, TEdgeData> ICell<TCoordinate, TNodeData, TEdgeData>.Map
		{
			get { return Map; }
		}

		public abstract IEnumerator<IAnt<TCoordinate, TNodeData, TEdgeData>> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Abstract Methods

		public abstract bool IsEmpty { get; }
		public abstract void Add(IAnt<TCoordinate, TNodeData, TEdgeData> ant);

		/// <exception cref="ArgumentOutOfRangeException"><paramref name="ant"/> does not contained in current <see cref="ICell{TCoordinate,TNodeData,TEdgeData}"/></exception>
		public abstract void Remove(IAnt<TCoordinate, TNodeData, TEdgeData> ant);

		#endregion
	}
}