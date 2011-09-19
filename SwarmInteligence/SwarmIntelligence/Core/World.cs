using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core
{
	public class World<TCoordinate, TNodeData, TEdgeData>
	{
		public World(DataLayer<TCoordinate, TNodeData> nodesData,
		             DataLayer<Edge<TCoordinate>, TEdgeData> edgesData,
		             IMap<TCoordinate, TNodeData, TEdgeData> map)
		{
			Requires.NotNull(nodesData, edgesData, map);
			Topology = map.Topology;
			NodesData = nodesData;
			EdgesData = edgesData;
			Map = map;
		}

		public Topology<TCoordinate> Topology { get; private set; }
		public DataLayer<TCoordinate, TNodeData> NodesData { get; private set; }
		public DataLayer<Edge<TCoordinate>, TEdgeData> EdgesData { get; private set; }
		public IMap<TCoordinate, TNodeData, TEdgeData> Map { get; private set; }
	}
}