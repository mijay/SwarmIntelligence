using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core
{
    public class World<C, B, E>
        where C: ICoordinate<C>
    {
        public World(Boundaries<C> boundaries, Topology<C> topology, NodeDataLayer<C, B> nodesData, EdgeDataLayer<C, E> edgesData)
        {
            Contract.Requires(boundaries != null && topology != null && nodesData != null && edgesData != null);
            Contract.Requires(topology.Boundaries == boundaries && nodesData.Boundaries == boundaries
                              && edgesData.Topology == topology);

            Boundaries = boundaries;
            Topology = topology;
            NodesData = nodesData;
            EdgesData = edgesData;
        }

        public Boundaries<C> Boundaries { get; set; }
        public Topology<C> Topology { get; set; }
        public NodeDataLayer<C, B> NodesData { get; set; }
        public EdgeDataLayer<C, E> EdgesData { get; set; }
    }
}