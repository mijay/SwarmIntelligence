using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Data;
using SwarmIntelligence.Implementation.Playground;

namespace SILibrary.BuildUp
{
	internal class BuildingWorld<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public ILog Log { get; set; }
		public Topology<TCoordinate> Topology { get; set; }
		public Map<TCoordinate, TNodeData, TEdgeData> Map { get; set; }
		public NodesDataLayer<TCoordinate, TNodeData> NodesData { get; set; }
		public EdgesDataLayer<TCoordinate, TEdgeData> EdgesData { get; set; }

		public World<TCoordinate, TNodeData, TEdgeData> Build()
		{
			return new World<TCoordinate, TNodeData, TEdgeData>(NodesData, EdgesData, Map, Log);
		}
	}
}