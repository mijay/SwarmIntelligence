using System.Diagnostics.Contracts;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SwarmIntelligence.Infrastructure.TurnProcessing
{
	public abstract class AntBase<TCoordinate, TNodeData, TEdgeData>: IAnt<TCoordinate, TNodeData, TEdgeData>
	{
		private bool removed;

		protected AntBase(World<TCoordinate, TNodeData, TEdgeData> world)
		{
			Contract.Requires(world != null);
			Outlook = new Outlook<TCoordinate, TNodeData, TEdgeData>(world, this);
		}

		internal Outlook<TCoordinate, TNodeData, TEdgeData> Outlook { get; private set; }

		internal void ProcessTurn(CellBase<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Contract.Requires(cell != null);
			if(removed)
				return;
			Outlook.CellBase = cell;
			Outlook.Coordinate = cell.Coordinate;
			ProcessTurn(Outlook);
		}

		public void Remove()
		{
			removed = true;
		}

		#region Implementation of IAnt<TCoordinate,TNodeData,TEdgeData>

		public abstract void ProcessTurn(IOutlook<TCoordinate, TNodeData, TEdgeData> outlook);

		#endregion
	}
}