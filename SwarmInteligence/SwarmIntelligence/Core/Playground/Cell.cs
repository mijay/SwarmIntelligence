using System.Collections;
using System.Collections.Generic;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	public abstract class Cell<TCoordinate, TNodeData, TEdgeData>: IEnumerable<Ant<TCoordinate, TNodeData, TEdgeData>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region Implementation of IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public abstract IEnumerator<Ant<TCoordinate, TNodeData, TEdgeData>> GetEnumerator();

		#endregion

		public abstract Map<TCoordinate, TNodeData, TEdgeData> Map { get; }
		public abstract TCoordinate Coordinate { get; }
	}
}