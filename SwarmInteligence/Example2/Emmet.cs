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
    internal class Emmet : AntBase<GraphCoordinate, EmptyData, DictionaryDataLayer<WeightEdge<GraphCoordinate>, double>>
    {
        private List<GraphCoordinate> _tabuList = new List<GraphCoordinate>();
        private readonly SortedDictionary<GraphCoordinate, double> _notVisitedVertex = new SortedDictionary<GraphCoordinate, double>();
        private double _denumerator;
        private readonly double _alpha;
        private readonly double _beta;
        private readonly double _k;
        private double _lenPath = 0;
        private static readonly Random Random = new Random();

        public Emmet(World<GraphCoordinate, EmptyData, DictionaryDataLayer<WeightEdge<GraphCoordinate>, double>> world, double alpha, double beta, double k) : base(world)
        {
            _alpha = alpha;
            _beta = beta;
            _k = k;
        }

        public override void ProcessTurn(IOutlook<GraphCoordinate, EmptyData, DictionaryDataLayer<WeightEdge<GraphCoordinate>, double>> outlook)
        {
            GetNotVisitedVertex(outlook);
            var point = Random.NextDouble();
            var sortNodes =
                (from entry in _notVisitedVertex orderby entry.Value ascending select entry).ToDictionary(
                    pair => pair.Key, pair => pair.Value);
            var coordinate = sortNodes.Where(sortNode => point < sortNode.Value).First().Key;
            this.MoveTo(coordinate);
            var edge = (WeightEdge<GraphCoordinate>) outlook.World.Topology.GetAdjacentEdges(outlook.Cell.Coordinate).Where(x => x.to.Equals(coordinate));
            _lenPath += edge.weight;

            outlook.World.EdgesData.Get(edge).Set(edge, _k / _lenPath);
        }

        private void GetNotVisitedVertex(IOutlook<GraphCoordinate, EmptyData, DictionaryDataLayer<WeightEdge<GraphCoordinate>, double>> outlook)
        {
            var adjacent = outlook.World.Topology.GetAdjacent(outlook.Cell.Coordinate).Where(x => !_tabuList.Contains(x));
            if (adjacent.Count() == 0)
            {
                _tabuList = new List<GraphCoordinate>();
                _lenPath = 0;
            }
            _denumerator = 0;
            foreach (var coordinate in adjacent)
            {
                var edge = ((GraphTopology)outlook.World.Topology).GetAdjacentEdge(outlook.Cell.Coordinate, coordinate);
                var t = Math.Pow(outlook.World.EdgesData.Get(edge).Get(edge), _alpha);
                var w = 1 / Math.Pow(edge.weight, _beta);
                var numerator = t + w;
                _notVisitedVertex.Add(coordinate, numerator);
                _denumerator += numerator;
            }

            var prevPart = 0.0;
            foreach (var node in _notVisitedVertex.Keys)
            {
                _notVisitedVertex[node] /= _denumerator;
                prevPart += _notVisitedVertex[node];
            }
        }
    }
}
