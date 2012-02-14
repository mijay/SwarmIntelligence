using System;
using System.Diagnostics.Contracts;

namespace SwarmIntelligence.Core.Space
{
	public struct Edge<TCoordinate>: IEquatable<Edge<TCoordinate>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public readonly TCoordinate from;
		public readonly TCoordinate to;

		public Edge(TCoordinate from, TCoordinate to)
		{
			Contract.Requires(!from.Equals(to));

			this.from = from;
			this.to = to;
		}

		#region IEquatable<Edge<TCoordinate>> Members

		public bool Equals(Edge<TCoordinate> other)
		{
			return other.from.Equals(from) && other.to.Equals(to);
		}

		#endregion
	}
}