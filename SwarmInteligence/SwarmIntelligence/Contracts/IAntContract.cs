using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IAnt<,,>))]
	public abstract class IAntContract<TCoordinate, TNodeData, TEdgeData>: IAnt<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region Implementation of IAnt<TCoordinate,TNodeData,TEdgeData>

		public TCoordinate Coordinate
		{
			get { throw new UreachableCodeException(); }
		}

		public ICell<TCoordinate, TNodeData, TEdgeData> Cell
		{
			get
			{
				Contract.Ensures(Contract.Result<ICell<TCoordinate, TNodeData, TEdgeData>>().Coordinate.Equals(Coordinate));
				throw new UreachableCodeException();
			}
		}

		public void ProcessTurn()
		{
			throw new UreachableCodeException();
		}

		#endregion
	}
}