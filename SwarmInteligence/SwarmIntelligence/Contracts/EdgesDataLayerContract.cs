using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IEdgesDataLayer<,>))]
	public class EdgesDataLayerContract<TCoordinate, TEdgeData>: IEdgesDataLayer<TCoordinate, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region IEdgesDataLayer<TCoordinate,TEdgeData> Members

		public bool TryGet(Edge<TCoordinate> key, out TEdgeData value)
		{
			Contract.EnsuresOnThrow<IndexOutOfRangeException>(!Topology.Lays(key));
			Contract.Ensures(!Contract.Result<bool>() || Topology.Lays(key));

			throw new UreachableCodeException();
		}

		public IEnumerator<KeyValuePair<Edge<TCoordinate>, TEdgeData>> GetEnumerator()
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