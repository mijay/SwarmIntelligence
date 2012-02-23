using Common;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Logging;

namespace SILibrary.BuildUp
{
	internal class InitialConfiguration<TCoordinate, TNodeData, TEdgeData>: DisposableBase,
	                                                                        SystemBuilder.ILogConfiguration<TCoordinate, TNodeData, TEdgeData>,
	                                                                        SystemBuilder.ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld;

		public InitialConfiguration(BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld)
		{
			this.buildingWorld = buildingWorld;
		}

		#region Implementation of ILogConfiguration<TCoordinate,TNodeData,TEdgeData>

		public SystemBuilder.ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData> WithLog(ILog log)
		{
			CheckDisposedState();
			buildingWorld.Log = log;
			return this;
		}

		public SystemBuilder.ITopologyConfiguration<TCoordinate, TNodeData, TEdgeData> WithDefaultLog(out ILogManager logManager)
		{
			CheckDisposedState();
			logManager = new LogManager();
			buildingWorld.Log = logManager.Log;
			return this;
		}

		#endregion

		#region Implementation of ITopologyConfiguration<TCoordinate,TNodeData,TEdgeData>

		public SystemBuilder.IMapConfiguration<TCoordinate, TNodeData, TEdgeData> WithTopology(Topology<TCoordinate> topology)
		{
			CheckDisposedState();
			buildingWorld.Topology = topology;
			Dispose();
			return new MapConfiguration<TCoordinate, TNodeData, TEdgeData>(buildingWorld);
		}

		#endregion
	}
}