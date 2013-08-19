using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Specialized
{
	[ContractClass(typeof(IMapModifierContract<,,>))]
	public interface IMapModifier<TCoordinate, TNodeData, TEdgeData>: IDisposable
		where TCoordinate: ICoordinate<TCoordinate>
	{
		[Pure]
		World<TCoordinate, TNodeData, TEdgeData> World { get; }

		void AddAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate);
		void RemoveAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate);
	}
}