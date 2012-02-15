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
	public abstract class EdgesDataLayerContract<TCoordinate, TEdgeData>: IEdgesDataLayer<TCoordinate, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region IEdgesDataLayer<TCoordinate,TEdgeData> Members

		public IEnumerator<KeyValuePair<Edge<TCoordinate>, TEdgeData>> GetEnumerator()
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

		public TEdgeData Get(Edge<TCoordinate> key)
		{
			Contract.Ensures(Topology.Lays(key));
			Contract.EnsuresOnThrow<IndexOutOfRangeException>(!Topology.Lays(key));
			throw new UnreachableCodeException();
		}

		#endregion
	}
}