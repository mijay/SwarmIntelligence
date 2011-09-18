using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core
{
	public class World<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public World(DataLayer<TCoordinate, TNodeData> nodesData,
		             DataLayer<Edge<TCoordinate>, TEdgeData> edgesData,
		             Map<TCoordinate, TNodeData, TEdgeData> map)
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
		public Map<TCoordinate, TNodeData, TEdgeData> Map { get; private set; }
	}
}