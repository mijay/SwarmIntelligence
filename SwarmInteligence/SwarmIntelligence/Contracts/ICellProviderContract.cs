using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SwarmIntelligence.Contracts
{
	public class ICellProviderContract<TCoordinate, TNodeData, TEdgeData>: ICellProvider<TCoordinate, TNodeData, TEdgeData>
	{
		#region Implementation of ICellProvider<TCoordinate,TNodeData,TEdgeData>

		public MapBase<TCoordinate, TNodeData, TEdgeData> Context
		{
			get { throw new UreachableCodeException(); }
			set
			{
				Contract.Requires(value != null);
				Contract.Requires(Context == null);
				Contract.Ensures(Context != null);
				throw new UreachableCodeException();
			}
		}

		public void Return(CellBase<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Contract.Requires(Context != null);
			Contract.Requires(cell != null);
			Contract.Requires(cell.Map == Context);
			throw new UreachableCodeException();
		}

		public CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate)
		{
			Contract.Requires(Context != null);
			Contract.Requires(Context.Topology.Lays(coordinate));
			Contract.Ensures(Contract.Result<CellBase<TCoordinate, TNodeData, TEdgeData>>() != null);
			Contract.Ensures(Contract.Result<CellBase<TCoordinate, TNodeData, TEdgeData>>().Map == Context);
			Contract.Ensures(Contract.Result<CellBase<TCoordinate, TNodeData, TEdgeData>>().Coordinate.Equals(coordinate));
			throw new UreachableCodeException();
		}

		#endregion
	}
}