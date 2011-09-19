namespace SwarmIntelligence.Core.Space
{
	public struct Edge<TCoordinate>
	{
		public readonly TCoordinate from;
		public readonly TCoordinate to;

		public Edge(TCoordinate from, TCoordinate to)
		{
			this.from = from;
			this.to = to;
		}
	}
}