using System.Linq;
using Common.Collections;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Infrastructure.TurnProcessing;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence
{
	public class Runner<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly MapBase<TCoordinate, TNodeData, TEdgeData> mapBase;
		private readonly World<TCoordinate, TNodeData, TEdgeData> world;

		public Runner(World<TCoordinate, TNodeData, TEdgeData> world)
		{
			this.world = world;
			mapBase = world.Map.Base();
		}

		public void DoTurn()
		{
			mapBase.OnTurnBegin();
			AntContext[] contexts = SelectContext();
			RunTurn(contexts);
			mapBase.OnTurnEnd();
		}

		private static void RunTurn(AntContext[] contexts)
		{
			contexts.ForEach(x => x.Ant.ProcessTurn(x.Cell));
		}

		private AntContext[] SelectContext()
		{
			return mapBase
				.AsParallel()
				.Select(x => x.Value.Base())
				.SelectMany(cellBase => cellBase.Select(ant => new AntContext { Ant = ant.Base(), Cell = cellBase }))
				.ToArray();
		}

		#region Nested type: AntContext

		private struct AntContext
		{
			public AntBase<TCoordinate, TNodeData, TEdgeData> Ant;
			public CellBase<TCoordinate, TNodeData, TEdgeData> Cell;
		}

		#endregion
	}
}