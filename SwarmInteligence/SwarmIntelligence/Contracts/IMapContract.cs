using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IMap<,,>))]
	public class IMapContract<TCoordinate, TNodeData, TEdgeData>: IMap<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region IMap<TCoordinate,TNodeData,TEdgeData> Members

		public bool TryGet(TCoordinate key, out ICell<TCoordinate, TNodeData, TEdgeData> value)
		{
			Contract.EnsuresOnThrow<IndexOutOfRangeException>(!Topology.Lays(key));
			Contract.Ensures(!Contract.Result<bool>() || Topology.Lays(key));

			throw new UreachableCodeException();
		}

		public IEnumerator<KeyValuePair<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>> GetEnumerator()
		{
			throw new UreachableCodeException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Topology<TCoordinate> Topology
		{
			get { throw new UreachableCodeException(); }
		}

		#endregion
	}
}