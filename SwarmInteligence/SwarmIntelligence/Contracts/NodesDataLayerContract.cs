using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(INodesDataLayer<,>))]
	public class NodesDataLayerContract<TCoordinate, TNodeData>: INodesDataLayer<TCoordinate, TNodeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region INodesDataLayer<TCoordinate,TNodeData> Members

		public bool TryGet(TCoordinate key, out TNodeData value)
		{
			Contract.EnsuresOnThrow<IndexOutOfRangeException>(!Topology.Lays(key));
			Contract.Ensures(!Contract.Result<bool>() || Topology.Lays(key));

			throw new UreachableCodeException();
		}

		public IEnumerator<KeyValuePair<TCoordinate, TNodeData>> GetEnumerator()
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