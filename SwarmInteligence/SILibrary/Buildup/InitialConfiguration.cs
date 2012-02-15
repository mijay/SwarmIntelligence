using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Logging;

namespace SILibrary.Buildup
{
	internal class InitialConfiguration<TCoordinate, TNodeData, TEdgeData>: ILogConfiguration<TCoordinate, TNodeData, TEdgeData>,
	                                                                        ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld;

		public InitialConfiguration(BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld)
		{
			this.buildingWorld = buildingWorld;
		}

		#region Implementation of ILogConfiguration<TCoordinate,TNodeData,TEdgeData>

		public ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData> WithLog(ILog log)
		{
			buildingWorld.Log = log;
			return this;
		}

		public ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultLog(out ILogManager logManager)
		{
			logManager = new LogManager();
			buildingWorld.Log = logManager.Log;
			return this;
		}

		#endregion

		#region Implementation of ITopologyConfiguration<TCoordinate,TNodeData,TEdgeData>

		public IMapConfiguration<TCoordinate, TNodeData, TEdgeData> WithTopology(Topology<TCoordinate> topology)
		{
			buildingWorld.Topology = topology;
			return new MapConfiguration<TCoordinate, TNodeData, TEdgeData>(buildingWorld);
		}

		#endregion
	}
}