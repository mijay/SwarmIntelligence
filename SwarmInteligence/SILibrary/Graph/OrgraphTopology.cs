using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using SwarmIntelligence.Core.Space;

namespace SILibrary.Graph
{
	public class OrgraphTopology: Topology<GraphCoordinate>
	{
		private readonly ILookup<GraphCoordinate, GraphCoordinate> adjacent;
		private readonly ISet<Edge<GraphCoordinate>> edges;
		private readonly ISet<GraphCoordinate> nodes;
		private readonly ILookup<GraphCoordinate, GraphCoordinate> predecessors;
		private readonly ILookup<GraphCoordinate, GraphCoordinate> successors;

		/// <summary>
		/// Creates the oriented graph without isolated nodes (node with degree equal to zero).
		/// </summary>
		public OrgraphTopology(ISet<Edge<GraphCoordinate>> edges)
			: this(new HashSet<GraphCoordinate>(edges.SelectMany(x => new[] { x.from, x.to })), edges)
		{
			Contract.Requires(edges != null);
		}

		public OrgraphTopology(ISet<GraphCoordinate> nodes, ISet<Edge<GraphCoordinate>> edges)
		{
			Contract.Requires(nodes != null && edges != null);
			Contract.Requires(Contract.ForAll(edges, edge => nodes.Contains(edge.from) && nodes.Contains(edge.to)));

			this.nodes = new HashSet<GraphCoordinate>(nodes);
			this.edges = new HashSet<Edge<GraphCoordinate>>(edges);

			successors = edges.ToLookup(x => x.from, x => x.to);
			predecessors = edges.ToLookup(x => x.to, x => x.from);
			adjacent = edges
				.SelectMany(x => new[]
				                 {
				                 	Tuple.Create(x.to, x.from),
				                 	Tuple.Create(x.from, x.to)
				                 })
				.ToLookup(x => x.Item1, x => x.Item2);
		}

		/// <summary>
		/// Creates the oriented graph by the adjacency list.
		/// All nodes will be numerated as 0..(nodes count - 1).
		/// Each element in adjacency list is edge described as (start node number, end node number).
		/// </summary>
		public static OrgraphTopology ByAdjacencyList(IEnumerable<Tuple<int, int>> adjacencyList)
		{
			Contract.Requires(adjacencyList != null);
			Contract.Requires(Contract.ForAll(adjacencyList, pair => pair.Item1 >= 0 && pair.Item2 >= 0));
			Contract.Requires(adjacencyList.Distinct().Count() == adjacencyList.Count());

			int maxNodeIndex = adjacencyList
				.SelectMany(x => new[] { x.Item1, x.Item2 })
				.Max();
			IEnumerable<GraphCoordinate> nodes = Enumerable
				.Range(0, maxNodeIndex + 1)
				.Select(x => new GraphCoordinate(x));
			IEnumerable<Edge<GraphCoordinate>> edges = adjacencyList
				.Select(x => new Edge<GraphCoordinate>(new GraphCoordinate(x.Item1),
				                                       new GraphCoordinate(x.Item2)));
			return new OrgraphTopology(new HashSet<GraphCoordinate>(nodes),
			                           new HashSet<Edge<GraphCoordinate>>(edges));
		}

		public override bool Lays(GraphCoordinate coord)
		{
			return nodes.Contains(coord);
		}

		public override IEnumerable<GraphCoordinate> GetSuccessors(GraphCoordinate coord)
		{
			return successors[coord];
		}

		public override IEnumerable<GraphCoordinate> GetPredecessors(GraphCoordinate coord)
		{
			return predecessors[coord];
		}

		public override IEnumerable<GraphCoordinate> GetAdjacent(GraphCoordinate coord)
		{
			return adjacent[coord];
		}

		public override bool Lays(Edge<GraphCoordinate> edge)
		{
			return edges.Contains(edge);
		}
	}
}