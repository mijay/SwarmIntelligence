namespace SwarmIntelligence2.Core.World
{
    public struct Edge<C>
        where C: ICoordinate<C>
    {
        public readonly C from;
        public readonly C to;

        public Edge(C from, C to): this()
        {
            this.from = from;
            this.to = to;
        }
    }
}