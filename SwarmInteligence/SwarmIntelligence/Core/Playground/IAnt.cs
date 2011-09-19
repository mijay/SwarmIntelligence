namespace SwarmIntelligence.Core.Playground
{
	public interface IAnt<TCoordinate, TNodeData, TEdgeData>
	{
		void ProcessTurn(IOutlook<TCoordinate, TNodeData, TEdgeData> outlook);
	}
}