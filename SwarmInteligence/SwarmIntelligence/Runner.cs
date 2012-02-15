using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections.Extensions;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Logging;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence
{
	public class Runner<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly Map<TCoordinate, TNodeData, TEdgeData> map;

		public Runner(World<TCoordinate, TNodeData, TEdgeData> world)
		{
			Contract.Requires(world != null);
			World = world;
			map = world.Map.Base();
		}

		public World<TCoordinate, TNodeData, TEdgeData> World { get; private set; }

		public void DoTurn()
		{
			World.Log.Log(CommonLogTypes.TurnStarted);
			AntContext[] contexts = SelectContext();
			RunTurn(contexts);
			World.Log.Log(CommonLogTypes.TurnDone);
		}

		private static void RunTurn(AntContext[] contexts)
		{
			contexts.ForEach(x => x.Ant.ProcessTurn(x.Cell));
		}

		private AntContext[] SelectContext()
		{
			return map
				.AsParallel()
				.Select(cell => cell.Value.Base())
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