using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Collections.Extensions;
using SILibrary.Base;
using SILibrary.Graph;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.Playground;

namespace Example2
{
	internal class Emmet: AntBase<GraphCoordinate, EmptyData, EdgeData>
	{
		private static readonly Random random = new Random();
		private readonly double alpha;
		private readonly double beta;
		private readonly double k;
		private readonly List<MutablePair<GraphCoordinate, double>> notVisitedVertex = new List<MutablePair<GraphCoordinate, double>>();
		private readonly HashSet<GraphCoordinate> tabuList = new HashSet<GraphCoordinate>();
		private double denumerator;
		private double lenPath;

		public Emmet(World<GraphCoordinate, EmptyData, EdgeData> world, double alpha, double beta, double k)
			: base(world)
		{
			this.alpha = alpha;
			this.beta = beta;
			this.k = k;
		}

		public override void ProcessTurn()
		{
			UpdateNotVisitedVertex();
			double point = random.NextDouble();
			GraphCoordinate coordinate = notVisitedVertex
				.OrderBy(entry => entry.Value)
				.First(sortedNode => point < sortedNode.Value)
				.Key;
			var edge = new Edge<GraphCoordinate>(Coordinate, coordinate);

			EdgeData data = World.EdgesData.Get(edge);
			lenPath += data.Weight;
			data.Odor += k / lenPath;

			this.MoveTo(coordinate);
			tabuList.Add(coordinate);
		}

		private void UpdateNotVisitedVertex()
		{
			Edge<GraphCoordinate>[] adjacentEdge = World.Topology
				.GetAdjacentEdges(Coordinate)
				.Where(x => !tabuList.Contains(x.to))
				.ToArray();
			if(adjacentEdge.IsEmpty()) {
				tabuList.Clear();
				lenPath = 0;
			}
			denumerator = 0;

			foreach(var edge in adjacentEdge) {
				EdgeData data = World.EdgesData.Get(edge);
				double t = Math.Pow(data.Odor, alpha);
				double w = 1 / Math.Pow(data.Weight, beta);
				double numerator = t + w;
				notVisitedVertex.Add(new MutablePair<GraphCoordinate, double>(edge.to, numerator));
			}

			double prevPart = 0.0;
			foreach(var t in notVisitedVertex) {
				t.Value /= denumerator;
				prevPart += t.Value;
			}
		}
	}
}