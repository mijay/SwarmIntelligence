using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.TurnProcessing;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence
{
	public static class Actions
	{
		public static void MoveTo<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase, TCoordinate to)
		{
			antBase.Outlook.CellBase.Remove(antBase);
			antBase.Outlook.Map.Get(to).Base().Add(antBase);
		}

		public static void AddTo<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                            IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
		{
			antBase.Outlook.Map.Get(coordinate).Base().Add(ant);
		}

		public static void RemoveFrom<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                                 IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
		{
			antBase.Outlook.Map.Get(coordinate).Base().Remove(ant);
		}
	}
}