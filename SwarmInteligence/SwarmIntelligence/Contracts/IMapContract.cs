using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IMap<,,>))]
	public abstract class IMapContract<TCoordinate, TNodeData, TEdgeData>: IMap<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region IMap<TCoordinate,TNodeData,TEdgeData> Members

		public bool TryGet(TCoordinate key, out ICell<TCoordinate, TNodeData, TEdgeData> value)
		{
			Contract.Ensures(Topology.Lays(key));
			Contract.EnsuresOnThrow<IndexOutOfRangeException>(!Topology.Lays(key));
			Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value).Coordinate.Equals(key));
			Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value).Map == this);
			Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value).Any());

			throw new UnreachableCodeException();
		}

		public IEnumerator<KeyValuePair<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>> GetEnumerator()
		{
			//todo!
			throw new UnreachableCodeException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Topology<TCoordinate> Topology
		{
			get
			{
				Contract.Ensures(Contract.Result<Topology<TCoordinate>>() != null);
				throw new UnreachableCodeException();
			}
		}

		#endregion
	}
}