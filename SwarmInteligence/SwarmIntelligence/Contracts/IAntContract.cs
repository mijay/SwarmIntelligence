﻿using System.Diagnostics.Contracts;
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

		public void ProcessTurn(IOutlook<TCoordinate, TNodeData, TEdgeData> outlook)
		{
			Contract.Requires(outlook != null && outlook.Me == this);
			throw new UreachableCodeException();
		}

		#endregion
	}
}