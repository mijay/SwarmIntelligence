using System.Collections.Generic;
using System.Linq;

namespace SwarmIntelligence.Core.Space
{
	public abstract class Topology<TCoordinate>
	{
		public abstract bool Lays(TCoordinate coord);

		public abstract IEnumerable<TCoordinate> GetSuccessors(TCoordinate coord);

		public abstract IEnumerable<TCoordinate> GetPredecessors(TCoordinate coord);

		public virtual bool Lays(Edge<TCoordinate> edge)
		{
			return Lays(edge.from) && Lays(edge.to)
			       && GetSuccessors(edge.from).Contains(edge.to);
		}

		public virtual IEnumerable<TCoordinate> GetAdjacent(TCoordinate coord)
		{
			return GetSuccessors(coord).Union(GetPredecessors(coord));
		}

		public IEnumerable<Edge<TCoordinate>> GetOutgoing(TCoordinate coord)
		{
			return GetSuccessors(coord).Select(x => new Edge<TCoordinate>(coord, x));
		}

		public IEnumerable<Edge<TCoordinate>> GetIncoming(TCoordinate coord)
		{
			return GetPredecessors(coord).Select(x => new Edge<TCoordinate>(x, coord));
		}

		public IEnumerable<Edge<TCoordinate>> GetAdjacentEdges(TCoordinate coord)
		{
			return GetOutgoing(coord).Concat(GetIncoming(coord));
		}
	}
}