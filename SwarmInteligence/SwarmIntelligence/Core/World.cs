using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core
{
    public class World<C, B, E>
        where C: ICoordinate<C>
    {
        public World(Boundaries<C> boundaries, Topology<C> topology, NodeDataLayer<C, B> nodesData,
                     EdgeDataLayer<C, E> edgesData, Map<C, B, E> map)
        {
            Contract.Requires(boundaries != null && topology != null && nodesData != null && edgesData != null && map != null);
            Contract.Requires(topology.Boundaries == boundaries && nodesData.Boundaries == boundaries
                              && edgesData.Topology == topology && map.Boundaries == boundaries);

            Boundaries = boundaries;
            Topology = topology;
            NodesData = nodesData;
            EdgesData = edgesData;
            Map = map;
        }

        public Boundaries<C> Boundaries { get; private set; }
        public Topology<C> Topology { get; private set; }
        public NodeDataLayer<C, B> NodesData { get; private set; }
        public EdgeDataLayer<C, E> EdgesData { get; private set; }
        public Map<C, B, E> Map { get; private set; }
    }
}