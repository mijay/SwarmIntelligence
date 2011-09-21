using System.Diagnostics.Contracts;

namespace SwarmIntelligence.Core.Space
{
	public struct Edge<TCoordinate>
	{
		public readonly TCoordinate from;
		public readonly TCoordinate to;

		public Edge(TCoordinate from, TCoordinate to)
		{
			Contract.Requires(!from.Equals(to));

			this.from = from;
			this.to = to;
		}
	}
}