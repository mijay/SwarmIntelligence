using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Infrastructure.TurnProcessing;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence
{
	public static class Actions
	{
		public static void MoveTo<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase, TCoordinate to)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			TCoordinate from = antBase.Outlook.CellBase.Coordinate;
			antBase.Outlook.CellBase.Remove(antBase);
			CellBase<TCoordinate, TNodeData, TEdgeData> targetCell = antBase.Outlook.MapBase.Get(to).Base();
			targetCell.Add(antBase);

			antBase.Outlook.CellBase = targetCell;
			antBase.Outlook.Coordinate = to;

			antBase.Outlook.Log.Log(CommonLogTypes.AntMoved, antBase, from, to);
		}

		public static void AddTo<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                            IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			antBase.Outlook.MapBase.Get(coordinate).Base().Add(ant);

			antBase.Outlook.Log.Log(CommonLogTypes.AntAdded, antBase, coordinate);
		}

		public static void RemoveFrom<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                                 TCoordinate coordinate,
		                                                                 IAnt<TCoordinate, TNodeData, TEdgeData> ant)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			antBase.Outlook.MapBase.Get(coordinate).Base().Remove(ant);
			ant.Base().Remove();

			antBase.Outlook.Log.Log(CommonLogTypes.AntRemoved, ant, coordinate);
		}
	}
}