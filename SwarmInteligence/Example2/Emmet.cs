using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILibrary.General.Background;
using SILibrary.Graph;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace Example2
{
    internal class Emmet : AntBase<GraphCoordinate, EmptyData, double>
    {
        private List<GraphCoordinate> tabuList = new List<GraphCoordinate>();
        private readonly SortedDictionary<GraphCoordinate, double> notVisitedVertex = new SortedDictionary<GraphCoordinate, double>();
        private double denumerator;
        private readonly double alpha;
        private readonly double beta;
        private double lenPath = 0;
        private double k;
        private static readonly Random Random = new Random();

        public Emmet(World<GraphCoordinate, EmptyData, double> world, double alpha, double beta, double k) : base(world)
        {
            this.alpha = alpha;
            this.beta = beta;
            this.k = k;
        }

        public override void ProcessTurn(IOutlook<GraphCoordinate, EmptyData, double> outlook)
        {
            GetNotVisitedVertex(outlook);
            var point = Random.NextDouble();
            var sortNodes =
                (from entry in notVisitedVertex orderby entry.Value ascending select entry).ToDictionary(
                    pair => pair.Key, pair => pair.Value);
            var coordinate = sortNodes.Where(sortNode => point < sortNode.Value).First().Key;
            this.MoveTo(coordinate);
            var edge = (WeightEdge<GraphCoordinate>) outlook.World.Topology.GetAdjacentEdges(outlook.Cell.Coordinate).Where(x => x.to.Equals(coordinate));
            lenPath += edge.weight;

            outlook.World.EdgesData.Set(edge, k / lenPath);
        }

        private void GetNotVisitedVertex(IOutlook<GraphCoordinate, EmptyData, double> outlook)
        {
            var adjacent = outlook.World.Topology.GetAdjacent(outlook.Cell.Coordinate).Where(x => !tabuList.Contains(x));
            if (adjacent.Count() == 0)
            {
                tabuList = new List<GraphCoordinate>();
                lenPath = 0;
            }
            denumerator = 0;
            foreach (var coordinate in adjacent)
            {
                var edge = ((GraphTopology)outlook.World.Topology).GetAdjacentEdge(outlook.Cell.Coordinate, coordinate);
                var t = Math.Pow(outlook.World.EdgesData.Get(edge), alpha);
                var w = 1 / Math.Pow(edge.weight, beta);
                var numerator = t + w;
                notVisitedVertex.Add(coordinate, numerator);
                denumerator += numerator;
            }

            var prevPart = 0.0;
            foreach (var node in notVisitedVertex.Keys)
            {
                notVisitedVertex[node] /= denumerator;
                prevPart += notVisitedVertex[node];
            }
        }
    }
}
