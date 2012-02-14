using System.Diagnostics.Contracts;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence.Infrastructure.TurnProcessing
{
	public class Outlook<TCoordinate, TNodeData, TEdgeData>: IOutlook<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public Outlook(World<TCoordinate, TNodeData, TEdgeData> world, IAnt<TCoordinate, TNodeData, TEdgeData> me)
		{
			Contract.Requires(world != null && me != null);
			World = world;
			MapBase = world.Map.Base();
			NodesData = world.NodesData;
			EdgesData = world.EdgesData;
			Log = world.Log;
			Me = me;
		}

		public CellBase<TCoordinate, TNodeData, TEdgeData> CellBase { get; internal set; }
		public MapBase<TCoordinate, TNodeData, TEdgeData> MapBase { get; private set; }

		#region Implementation of IOutlook<TCoordinate,TNodeData,TEdgeData>

		public World<TCoordinate, TNodeData, TEdgeData> World { get; private set; }
		public DataLayer<TCoordinate, TNodeData> NodesData { get; private set; }
		public DataLayer<Edge<TCoordinate>, TEdgeData> EdgesData { get; private set; }
		public IAnt<TCoordinate, TNodeData, TEdgeData> Me { get; private set; }
		public TCoordinate Coordinate { get; internal set; }
		public ILog Log { get; internal set; }

		public IMap<TCoordinate, TNodeData, TEdgeData> Map
		{
			get { return MapBase; }
		}

		public ICell<TCoordinate, TNodeData, TEdgeData> Cell
		{
			get { return CellBase; }
		}

		#endregion
	}
}