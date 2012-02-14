using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	[ContractClass(typeof(IAntContract<,,>))]
	public interface IAnt<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		void ProcessTurn(IOutlook<TCoordinate, TNodeData, TEdgeData> outlook);
	}
}