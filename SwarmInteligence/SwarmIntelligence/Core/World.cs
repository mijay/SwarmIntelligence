using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core
{
	public class World<TCoordinate, TNodeData, TEdgeData>
	{
		public World(DataLayer<TCoordinate, TNodeData> nodesData,
		             DataLayer<Edge<TCoordinate>, TEdgeData> edgesData,
		             IMap<TCoordinate, TNodeData, TEdgeData> map,
		             ILog log)
		{
			Contract.Requires(map != null && edgesData != null && nodesData != null && log != null);
			Topology = map.Topology;
			NodesData = nodesData;
			EdgesData = edgesData;
			Map = map;
			Log = log;
		}

		public Topology<TCoordinate> Topology { get; private set; }
		public DataLayer<TCoordinate, TNodeData> NodesData { get; private set; }
		public DataLayer<Edge<TCoordinate>, TEdgeData> EdgesData { get; private set; }
		public IMap<TCoordinate, TNodeData, TEdgeData> Map { get; private set; }
		public ILog Log { get; private set; }
	}
}