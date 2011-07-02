using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.DataLayer;
using SwarmIntelligence.Core.PlayingField;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core
{
    public class World<C, B, E>
        where C: ICoordinate<C>
    {
        public World(Topology<C> topology, NodeDataLayer<C, B> nodesData,
                     EdgeDataLayer<C, E> edgesData, Map<C, B, E> map)
        {
            Contract.Requires(topology != null && nodesData != null && edgesData != null && map != null);
            Contract.Requires(nodesData.Topology == topology && edgesData.Topology == topology
                              && map.Topology == topology);

            Topology = topology;
            NodesData = nodesData;
            EdgesData = edgesData;
            Map = map;
        }

        public Topology<C> Topology { get; private set; }
        public NodeDataLayer<C, B> NodesData { get; private set; }
        public EdgeDataLayer<C, E> EdgesData { get; private set; }
        public Map<C, B, E> Map { get; private set; }
    }
}