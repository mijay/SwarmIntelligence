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
			var targetCell = antBase.Outlook.MapBase.Get(to).Base();
			targetCell.Add(antBase);
			antBase.Outlook.CellBase = targetCell;
			antBase.Outlook.Coordinate = to;
		}

		public static void AddTo<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                            IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
		{
			antBase.Outlook.MapBase.Get(coordinate).Base().Add(ant);
		}

		public static void RemoveFrom<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                                 IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
		{
			antBase.Outlook.MapBase.Get(coordinate).Base().Remove(ant);
		}
	}
}