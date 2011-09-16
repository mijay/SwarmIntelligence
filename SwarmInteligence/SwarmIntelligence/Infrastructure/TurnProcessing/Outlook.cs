using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SwarmIntelligence.Infrastructure.TurnProcessing
{
	public class Outlook<TCoordinate, TNodeData, TEdgeData>: IOutlook<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public Outlook(World<TCoordinate, TNodeData, TEdgeData> world, Ant<TCoordinate, TNodeData, TEdgeData> me)
		{
			World = world;
			Me = me;
		}

		public CellBase<TCoordinate, TNodeData, TEdgeData> CellBase { get; set; }

		#region Implementation of IOutlook<TCoordinate,TNodeData,TEdgeData>

		public World<TCoordinate, TNodeData, TEdgeData> World { get; private set; }
		public Ant<TCoordinate, TNodeData, TEdgeData> Me { get; private set; }
		public TCoordinate Coordinate { get; set; }

		public Cell<TCoordinate, TNodeData, TEdgeData> Cell
		{
			get { return CellBase; }
		}

		#endregion
	}
}