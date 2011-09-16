using System;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Infrastructure.TurnProcessing;

namespace SwarmIntelligence.Internal
{
	internal static class Helpers
	{
		public static CellBase<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this Cell<TCoordinate, TNodeData, TEdgeData> cell)
			where TCoordinate: ICoordinate<TCoordinate>
		{
#if DEBUG
			if(!(cell is CellBase<TCoordinate, TNodeData, TEdgeData>))
				throw new InvalidOperationException(string.Format("Cell of wrong type {0} found!!", cell.GetType()));
#endif
			return (CellBase<TCoordinate, TNodeData, TEdgeData>) cell;
		}

		public static AntBase<TCoordinate, TNodeData, TEdgeData> Base<TCoordinate, TNodeData, TEdgeData>(
			this Ant<TCoordinate, TNodeData, TEdgeData> ant)
			where TCoordinate: ICoordinate<TCoordinate>
		{
#if DEBUG
			if(!(ant is AntBase<TCoordinate, TNodeData, TEdgeData>))
				throw new InvalidOperationException(string.Format("Ant of wrong type {0} found!!", ant.GetType()));
#endif
			return (AntBase<TCoordinate, TNodeData, TEdgeData>) ant;
		}
	}
}