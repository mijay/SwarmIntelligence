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

			throw new UnreachableCodeException();
		}

		public IEnumerator<KeyValuePair<Edge<TCoordinate>, TEdgeData>> GetEnumerator()
		{
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

		public TEdgeData Get(Edge<TCoordinate> key)
		{
			Contract.Requires(Topology.Lays(key));
			throw new UnreachableCodeException();
		}

		#endregion
	}
}