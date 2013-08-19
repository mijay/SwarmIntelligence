using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections.Extensions;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
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
			var selectContext = SelectContext();
			selectContext.ForEach(x => x.ProcessTurn());
			World.Log.Log(CommonLogTypes.TurnDone);
		}

		private IAnt<TCoordinate, TNodeData, TEdgeData>[] SelectContext()
		{
			return map
				.AsParallel()
				.SelectMany(cell => cell.Value)
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