using Common;
using SILibrary.Empty;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Data;

namespace SILibrary.BuildUp
{
	internal class DataLayersConfiguration<TCoordinate, TNodeData, TEdgeData>: DisposableBase,
	                                                                           SystemBuilder.INodeDataConfiguration<TCoordinate, TNodeData, TEdgeData>,
	                                                                           SystemBuilder.IEdgeDataConfiguration<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld;

		public DataLayersConfiguration(BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld)
		{
			this.buildingWorld = buildingWorld;
		}

		#region Implementation of INodeDataConfiguration<TCoordinate,TNodeData,TEdgeData>

		public SystemBuilder.IEdgeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithEmptyNodeData()
		{
			return WithNodeData((ICompleteMapping<TCoordinate, TNodeData>) new EmptyMapping<TCoordinate>());
		}

		public SystemBuilder.IEdgeDataConfiguration<TCoordinate, TNodeData, TEdgeData> WithNodeData(
			ICompleteMapping<TCoordinate, TNodeData> nodeData)
		{
			CheckDisposedState();
			buildingWorld.NodesData = new NodesDataLayer<TCoordinate, TNodeData>(buildingWorld.Topology, nodeData);
			return this;
		}

		#endregion

		#region Implementation of IEdgeDataConfiguration<TCoordinate,TNodeData,TEdgeData>

		public SystemBuilder.IConfigured<TCoordinate, TNodeData, TEdgeData> WithEmptyEdgeData()
		{
			return WithEdgeData((ICompleteMapping<Edge<TCoordinate>, TEdgeData>) new EmptyMapping<Edge<TCoordinate>>());
		}

		public SystemBuilder.IConfigured<TCoordinate, TNodeData, TEdgeData> WithEdgeData(ICompleteMapping<Edge<TCoordinate>, TEdgeData> nodeData)
		{
			CheckDisposedState();
			buildingWorld.EdgesData = new EdgesDataLayer<TCoordinate, TEdgeData>(buildingWorld.Topology, nodeData);
			Dispose();
			return new Configured<TCoordinate, TNodeData, TEdgeData>(buildingWorld);
		}

		#endregion
	}
}