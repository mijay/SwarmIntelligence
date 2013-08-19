using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Playground;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(CellBase<,,>))]
	public class CellBaseContract<TCoordinate, TNodeData, TEdgeData>: CellBase<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public CellBaseContract(Map<TCoordinate, TNodeData, TEdgeData> map, TCoordinate coordinate)
			: base(map, coordinate)
		{
		}

		#region Overrides of CellBase<TCoordinate,TNodeData,TEdgeData>

		public override bool IsEmpty
		{
			get { throw new UnreachableCodeException(); }
		}

		public override IEnumerator<IAnt<TCoordinate, TNodeData, TEdgeData>> GetEnumerator()
		{
			throw new UnreachableCodeException();
		}

		public override void Add(IAnt<TCoordinate, TNodeData, TEdgeData> ant)
		{
			Contract.Requires(ant != null);
			Contract.Requires(ant.Coordinate.Equals(Coordinate) && ant.Cell == this);
			throw new UnreachableCodeException();
		}

		public override void Remove(IAnt<TCoordinate, TNodeData, TEdgeData> ant)
		{
			Contract.Requires(ant != null);
			Contract.Requires(ant.Coordinate.Equals(Coordinate) && ant.Cell == this);
			throw new UnreachableCodeException();
		}

		#endregion
	}
}