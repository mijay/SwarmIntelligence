using System;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IEdgesDataLayer<,>))]
	public abstract class IEdgesDataLayerContract<TCoordinate, TEdgeData>: IEdgesDataLayer<TCoordinate, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region IEdgesDataLayer<TCoordinate,TEdgeData> Members
		
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

		public void Set(Edge<TCoordinate> key, TEdgeData value)
		{
			Contract.Ensures(Topology.Lays(key));
			Contract.EnsuresOnThrow<IndexOutOfRangeException>(!Topology.Lays(key));
			throw new UnreachableCodeException();
		}

		#endregion
	}
}