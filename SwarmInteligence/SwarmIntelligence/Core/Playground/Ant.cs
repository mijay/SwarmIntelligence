using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	public abstract class Ant<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		protected abstract void ProcessTurn(IOutlook<TCoordinate, TNodeData, TEdgeData> outlook);
	}
}