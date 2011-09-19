using System;
using SwarmIntelligence.Core.Playground;

namespace SwarmIntelligence.Specialized
{
	public interface IMapModifier<TCoordinate, TNodeData, TEdgeData>: IDisposable
	{
		void AddAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate);
		void RemoveAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate);
	}
}