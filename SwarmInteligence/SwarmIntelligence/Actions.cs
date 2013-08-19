using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Logging;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence
{
	public static class Actions
	{
		public static void MoveTo<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase, TCoordinate to)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			TCoordinate from = antBase.Coordinate;
			antBase.Cell.Base().Remove(antBase);

			CellBase<TCoordinate, TNodeData, TEdgeData> targetCell = antBase.World.Map.Base().ForcedGet(to);
			antBase.GotoCell(targetCell);
			targetCell.Add(antBase);

			antBase.Log.Log(CommonLogTypes.AntMoved, antBase, from, to);
		}

		public static void AddTo<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                            IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			var cellBase = antBase.World.Map.Base().ForcedGet(coordinate);
			ant.Base().GotoCell(cellBase);
			cellBase.Add(ant);

			antBase.Log.Log(CommonLogTypes.AntAdded, antBase, coordinate);
		}

		public static void Kill<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                           IAnt<TCoordinate, TNodeData, TEdgeData> ant)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			antBase.World.Map.Base().ForcedGet(ant.Coordinate).Remove(ant);
			ant.Base().Remove();

			antBase.Log.Log(CommonLogTypes.AntRemoved, ant, ant.Coordinate);
		}

		public static void Die<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			Kill(antBase, antBase);
		}
	}
}