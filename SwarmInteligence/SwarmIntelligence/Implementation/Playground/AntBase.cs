using System.Diagnostics.Contracts;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Implementation.Playground
{
	public abstract class AntBase<TCoordinate, TNodeData, TEdgeData>: IAnt<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private bool removed;

		protected AntBase(World<TCoordinate, TNodeData, TEdgeData> world)
		{
			Contract.Requires(world != null);
			Log = world.Log;
			World = world;
		}

		protected internal World<TCoordinate, TNodeData, TEdgeData> World { get; private set; }
		protected internal ILog Log { get; private set; }

		internal void GotoCell(CellBase<TCoordinate, TNodeData, TEdgeData> cellBase)
		{
			Contract.Requires(cellBase != null && cellBase.Map == World.Map);
			Cell = cellBase;
			Coordinate = cellBase.Coordinate;
		}

		internal void Remove()
		{
			removed = true;
		}

		#region Implementation of IAnt<TCoordinate,TNodeData,TEdgeData>

		public abstract void ProcessTurn();
		public TCoordinate Coordinate { get; private set; }
		public ICell<TCoordinate, TNodeData, TEdgeData> Cell { get; private set; }

		#endregion
	}
}