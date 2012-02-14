using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.Playground
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

		internal void ProcessTurn(CellBase<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Contract.Requires(cell != null && cell.Map == World.Map);
			if(removed)
				return;
			Cell = cell;
			Coordinate = cell.Coordinate;
			ProcessTurn();
		}

		internal void Remove()
		{
			removed = true;
		}

		#region Implementation of IAnt<TCoordinate,TNodeData,TEdgeData>

		public abstract void ProcessTurn();
		public TCoordinate Coordinate { get; internal set; }
		public ICell<TCoordinate, TNodeData, TEdgeData> Cell { get; internal set; }

		#endregion
	}
}