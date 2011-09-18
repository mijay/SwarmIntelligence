using Common;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SwarmIntelligence.Infrastructure.TurnProcessing
{
	public abstract class AntBase<TCoordinate, TNodeData, TEdgeData>: Ant<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		internal readonly Outlook<TCoordinate, TNodeData, TEdgeData> outlook;

		protected AntBase(World<TCoordinate, TNodeData, TEdgeData> world)
		{
			Requires.NotNull(world);
			outlook = new Outlook<TCoordinate, TNodeData, TEdgeData>(world, this);
		}

		internal void ProcessTurn(CellBase<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Requires.NotNull(cell);
			outlook.CellBase = cell;
			outlook.Coordinate = cell.Coordinate;
			ProcessTurn(outlook);
		}
	}
}