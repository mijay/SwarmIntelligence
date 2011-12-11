using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using SwarmIntelligence.Core.Space;

namespace SILibrary.Graph
{
	public class GraphTopology: OrgraphTopology
	{
		public GraphTopology(ISet<Edge<GraphCoordinate>> edges)
			: base(edges)
		{
			Contract.Requires(Contract.ForAll(edges, edge => edges.Contains(new Edge<GraphCoordinate>(edge.to, edge.from))));|
		}

		public GraphTopology(ISet<GraphCoordinate> nodes, ISet<Edge<GraphCoordinate>> edges)
			: base(nodes, edges)
		{
			Contract.Requires(Contract.ForAll(edges, edge => edges.Contains(new Edge<GraphCoordinate>(edge.to, edge.from))));|
		}

		/// <summary>
		/// Creates the oriented graph by the adjacency list.
		/// Each element in adjacency list is connection between two edges.
		/// </summary>
		public static GraphTopology ByAdjacencyList(IEnumerable<Tuple<int, int>> adjacencyList)
		{
			Contract.Requires(adjacencyList != null);
			Contract.Requires(Contract.ForAll(adjacencyList, pair => pair.Item1 >= 0 && pair.Item2 >= 0));
			Contract.Requires(adjacencyList.Distinct().Count() == adjacencyList.Count());

			IEnumerable<Edge<GraphCoordinate>> edges = adjacencyList
				.SelectMany(x => new[]
				                 {
				                 	new Edge<GraphCoordinate>(new GraphCoordinate(x.Item1),
				                 	                          new GraphCoordinate(x.Item2)),
				                 	new Edge<GraphCoordinate>(new GraphCoordinate(x.Item2),
				                 	                          new GraphCoordinate(x.Item1))
				                 });
			return new GraphTopology(new HashSet<Edge<GraphCoordinate>>(edges));
		}
	}
}