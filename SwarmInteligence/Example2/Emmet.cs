using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using SILibrary.General.Background;
using SILibrary.Graph;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace Example2
{
    internal class Emmet : AntBase<GraphCoordinate, EmptyData, TupleDataEdge>
    {
        private List<GraphCoordinate> tabuList = new List<GraphCoordinate>();
        private List<MutablePair<GraphCoordinate, double >> notVisitedVertex = new List<MutablePair<GraphCoordinate, double>>();
        private double denumerator;
        private readonly double alpha;
        private readonly double beta;
        private double lenPath = 0;
        private double k;
        private static readonly Random Random = new Random();

        public Emmet(World<GraphCoordinate, EmptyData, TupleDataEdge> world, double alpha, double beta, double k) : base(world)
        {
            this.alpha = alpha;
            this.beta = beta;
            this.k = k;
        }

        public override void ProcessTurn(IOutlook<GraphCoordinate, EmptyData, TupleDataEdge> outlook)
        {
            GetNotVisitedVertex(outlook);
            var point = Random.NextDouble();
            var coordinate =
                notVisitedVertex.OrderBy(entry => entry.Value).First(sortedNode => point < sortedNode.Value).Key;
            this.MoveTo(coordinate);
            var edge = new Edge<GraphCoordinate>(outlook.Cell.Coordinate, coordinate);
            lenPath += outlook.World.EdgesData.Get(edge).Weight;

            outlook.World.EdgesData[edge] = new TupleDataEdge(outlook.World.EdgesData.Get(edge).Weight,
                                                              outlook.World.EdgesData.Get(edge).Odor + k/lenPath);
            tabuList.Add(coordinate);
        }

        private void GetNotVisitedVertex(IOutlook<GraphCoordinate, EmptyData, TupleDataEdge> outlook)
        {
            var adjacentEdge = outlook.World.Topology.GetAdjacentEdges(outlook.Cell.Coordinate).Where(x => !tabuList.Contains(x.to));
            if (adjacentEdge.Count() == 0)
            {
                tabuList.Clear();
                lenPath = 0;
            }
            denumerator = 0;

            foreach (var edge in adjacentEdge)
            {
                var t = Math.Pow(outlook.World.EdgesData.Get(edge).Odor, alpha);
                var w = 1/Math.Pow(outlook.World.EdgesData.Get(edge).Weight, beta);
                var numerator = t + w;
                notVisitedVertex.Add(new MutablePair<GraphCoordinate, double>(edge.to, numerator));
            }

            var prevPart = 0.0;
            foreach (var t in notVisitedVertex)
            {
                t.Value /= denumerator;
                prevPart += t.Value;
            }
        }
    }
}
