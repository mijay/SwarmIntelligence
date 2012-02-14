using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core
{
	public class World<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public World(INodesDataLayer<TCoordinate, TNodeData> nodesData,
		             IEdgesDataLayer<TCoordinate, TEdgeData> edgesData,
		             IMap<TCoordinate, TNodeData, TEdgeData> map,
		             ILog log)
		{
			Contract.Requires(map != null && edgesData != null && nodesData != null && log != null);
			Contract.Requires(map.Topology == nodesData.Topology && map.Topology == edgesData.Topology);
			Topology = map.Topology;
			NodesData = nodesData;
			EdgesData = edgesData;
			Map = map;
			Log = log;
		}

		public Topology<TCoordinate> Topology { get; private set; }
		public INodesDataLayer<TCoordinate, TNodeData> NodesData { get; private set; }
		public IEdgesDataLayer<TCoordinate, TEdgeData> EdgesData { get; private set; }
		public IMap<TCoordinate, TNodeData, TEdgeData> Map { get; private set; }
		public ILog Log { get; private set; }
	}
}