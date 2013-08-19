using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections.Extensions;
using SwarmIntelligence.Contracts;

namespace SwarmIntelligence.Core.Space
{
	/// <summary>
	/// Class that represents the topology of the space specified by coordinates of type <typeparamref name="TCoordinate"/>
	/// </summary>
	/// <typeparam name="TCoordinate">Type of coordinates in represented space.</typeparam>
	[ContractClass(typeof(TopologyContract<>))]
	public abstract class Topology<TCoordinate>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		/// <summary>
		/// Checks if the <paramref name="coord"/> lays in space.
		/// </summary>
		[Pure]
		public abstract bool Lays(TCoordinate coord);

		[Pure]
		public abstract IEnumerable<TCoordinate> GetSuccessors(TCoordinate coord);

		[Pure]
		public abstract IEnumerable<TCoordinate> GetPredecessors(TCoordinate coord);

		[Pure]
		public virtual bool Lays(Edge<TCoordinate> edge)
		{
			Contract.Ensures(Contract.Result<bool>() == Lays(new Edge<TCoordinate>(edge.to, edge.from)));

			return Lays(edge.from) && Lays(edge.to)
			       && GetSuccessors(edge.from).Contains(edge.to);
		}

		/// <remarks>Performance critical that method should be overridden.</remarks>
		[Pure]
		public virtual IEnumerable<TCoordinate> GetAdjacent(TCoordinate coord)
		{
			Contract.Requires(Lays(coord));
			Contract.Ensures(!Contract.Result<IEnumerable<TCoordinate>>().IsEmpty());

			return GetSuccessors(coord).Union(GetPredecessors(coord));
		}

		[Pure]
		public IEnumerable<Edge<TCoordinate>> GetOutgoing(TCoordinate coord)
		{
			Contract.Requires(Lays(coord));
			return GetSuccessors(coord).Select(x => new Edge<TCoordinate>(coord, x));
		}

		[Pure]
		public IEnumerable<Edge<TCoordinate>> GetIncoming(TCoordinate coord)
		{
			Contract.Requires(Lays(coord));
			return GetPredecessors(coord).Select(x => new Edge<TCoordinate>(x, coord));
		}

		[Pure]
		public IEnumerable<Edge<TCoordinate>> GetAdjacentEdges(TCoordinate coord)
		{
			Contract.Requires(Lays(coord));
			return GetOutgoing(coord).Concat(GetIncoming(coord));
		}
	}
}