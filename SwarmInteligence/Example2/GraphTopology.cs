using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILibrary.General;
using SwarmIntelligence.Core.Space;

namespace Example2
{
    public class GraphTopology: BiConnectedTopologyBase<SimpleCoordinates>
    {
        private List<SimpleCoordinates> vertexes = new List<SimpleCoordinates>();
        private Dictionary<SimpleCoordinates, Dictionary<SimpleCoordinates, int>> edges = new Dictionary<SimpleCoordinates, Dictionary<SimpleCoordinates, int>>();

        public GraphTopology(List<SimpleCoordinates> vertexes, Dictionary<SimpleCoordinates, Dictionary<SimpleCoordinates, int>> edges)
        {
            this.vertexes = vertexes;
            this.edges = edges;
        }

        public override bool Lays(SimpleCoordinates coord)
        {
            return vertexes.Contains(coord);
        }

        protected override IEnumerable<SimpleCoordinates> GetNeighbours(SimpleCoordinates coord)
        {
            var neighbours = edges[coord];

            return (from neighbour in neighbours where neighbour.Value != 0 select neighbour.Key).ToList();
        }
}
}
