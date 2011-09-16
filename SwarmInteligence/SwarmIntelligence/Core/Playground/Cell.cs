using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	public abstract class Cell<TCoordinate, TNodeData, TEdgeData>: IEnumerable<Ant<TCoordinate, TNodeData, TEdgeData>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		protected Cell(Map<TCoordinate, TNodeData, TEdgeData> map, TCoordinate coordinate)
		{
			//todo: может привязка к координате (а значит и карте) вредна с точки зрения производительности?
			Contract.Requires(map != null && map.Topology.Lays(coordinate));

			Map = map;
			Coordinate = coordinate;
		}

		#region Implementation of IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public abstract IEnumerator<Ant<TCoordinate, TNodeData, TEdgeData>> GetEnumerator();

		#endregion

		public Map<TCoordinate, TNodeData, TEdgeData> Map { get; private set; }
		public TCoordinate Coordinate { get; private set; }

		public abstract void Add(Ant<TCoordinate, TNodeData, TEdgeData> ant);
		public abstract bool Remove(Ant<TCoordinate, TNodeData, TEdgeData> ant);
	}
}