using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	[ContractClass(typeof(ICellProviderContract<,,>))]
	public interface ICellProvider<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		MapBase<TCoordinate, TNodeData, TEdgeData> Context { [Pure] get; set; }

		void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell);
		CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate);
	}
}