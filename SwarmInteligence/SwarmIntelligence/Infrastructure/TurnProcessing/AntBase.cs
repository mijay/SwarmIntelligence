using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.TurnProcessing
{
	public abstract class AntBase<TCoordinate, TNodeData, TEdgeData> : Ant<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		internal readonly Outlook<TCoordinate, TNodeData, TEdgeData> outlook;

		protected AntBase(World<TCoordinate, TNodeData, TEdgeData> world)
		{
			outlook = new Outlook<TCoordinate, TNodeData, TEdgeData>(world, this);
		}

		internal void ProcessTurn(TCoordinate coordinate, Cell<TCoordinate, TNodeData, TEdgeData> cell)
		{
			outlook.Coordinate = coordinate;
			outlook.Cell = cell;
			ProcessTurn(outlook);
		}
	}
}