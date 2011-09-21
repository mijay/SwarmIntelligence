using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;

namespace SwarmIntelligence.Core.Playground
{
	[ContractClass(typeof(IAntContract<,,>))]
	public interface IAnt<TCoordinate, TNodeData, TEdgeData>
	{
		void ProcessTurn(IOutlook<TCoordinate, TNodeData, TEdgeData> outlook);
	}
}