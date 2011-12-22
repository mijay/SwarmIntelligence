using System.Diagnostics.Contracts;

namespace SwarmIntelligence.Core.Space
{
	public class Edge<TCoordinate>
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

    public class WeightEdge<TCoordinate>: Edge<TCoordinate>
    {
        public readonly double weight;

        public WeightEdge(TCoordinate from, TCoordinate to, double weight) : base(from, to)
        {
            this.weight = weight;
        }
    }
}