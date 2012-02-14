using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections.Extensions;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.GrabgeCollection;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Infrastructure.TurnProcessing;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence
{
	public class Runner<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly IGarbageCollector<TCoordinate, TNodeData, TEdgeData> garbageCollector;
		private readonly MapBase<TCoordinate, TNodeData, TEdgeData> mapBase;

		public Runner(World<TCoordinate, TNodeData, TEdgeData> world, IGarbageCollector<TCoordinate, TNodeData, TEdgeData> garbageCollector)
		{
			Contract.Requires(world != null && garbageCollector != null && garbageCollector.MapBase == null);
			World = world;
			mapBase = world.Map.Base();
			this.garbageCollector = garbageCollector;
			garbageCollector.AttachTo(mapBase);
		}

		public World<TCoordinate, TNodeData, TEdgeData> World { get; private set; }

		public void DoTurn()
		{
			World.Log.Log(CommonLogTypes.TurnStarted);
			AntContext[] contexts = SelectContext();
			RunTurn(contexts);
			garbageCollector.Collect();
			World.Log.Log(CommonLogTypes.TurnDone);
		}

		private static void RunTurn(AntContext[] contexts)
		{
			contexts.ForEach(x => x.Ant.ProcessTurn(x.Cell));
		}

		private AntContext[] SelectContext()
		{
			return mapBase
				.AsParallel()
				.Select(cell => cell.Base())
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