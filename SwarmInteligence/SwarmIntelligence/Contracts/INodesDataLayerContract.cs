using System;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(INodesDataLayer<,>))]
	public abstract class INodesDataLayerContract<TCoordinate, TNodeData>: INodesDataLayer<TCoordinate, TNodeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region INodesDataLayer<TCoordinate,TNodeData> Members
		
		public Topology<TCoordinate> Topology
		{
			get
			{
				Contract.Ensures(Contract.Result<Topology<TCoordinate>>() != null);
				throw new UnreachableCodeException();
			}
		}

		public TNodeData Get(TCoordinate key)
		{
			Contract.Ensures(Topology.Lays(key));
			Contract.EnsuresOnThrow<IndexOutOfRangeException>(!Topology.Lays(key));
			throw new UnreachableCodeException();
		}

		public void Set(TCoordinate key, TNodeData value)
		{
			Contract.Ensures(Topology.Lays(key));
			Contract.EnsuresOnThrow<IndexOutOfRangeException>(!Topology.Lays(key));
			throw new UnreachableCodeException();
		}

		#endregion
	}
}