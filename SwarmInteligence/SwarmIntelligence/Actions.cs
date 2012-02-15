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
			CellBase<TCoordinate, TNodeData, TEdgeData> targetCell = antBase.World.Map.Base().Get(to);
			targetCell.Add(antBase);

			antBase.Cell = targetCell;
			antBase.Coordinate = to;

			antBase.Log.Log(CommonLogTypes.AntMoved, antBase, from, to);
		}

		public static void AddTo<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                            IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			antBase.World.Map.Base().Get(coordinate).Add(ant);

			antBase.Log.Log(CommonLogTypes.AntAdded, antBase, coordinate);
		}

		public static void Kill<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase,
		                                                           IAnt<TCoordinate, TNodeData, TEdgeData> ant)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			antBase.World.Map.Base().Get(ant.Coordinate).Remove(ant);
			ant.Base().Remove();

			antBase.Log.Log(CommonLogTypes.AntRemoved, ant);
		}

		public static void Die<TCoordinate, TNodeData, TEdgeData>(this AntBase<TCoordinate, TNodeData, TEdgeData> antBase)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			Kill(antBase, antBase);
		}
	}
}