using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;
using SwarmIntelligence.Core;
using SwarmIntelligence.Infrastructure.GrabgeCollection;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Infrastructure.TurnProcessing;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence
{
	public class Runner<TCoordinate, TNodeData, TEdgeData>
	{
		private readonly MapBase<TCoordinate, TNodeData, TEdgeData> mapBase;
		private readonly World<TCoordinate, TNodeData, TEdgeData> world;
		private readonly IGarbageCollector<TCoordinate, TNodeData, TEdgeData> garbageCollector;

		public Runner(World<TCoordinate, TNodeData, TEdgeData> world, IGarbageCollector<TCoordinate, TNodeData, TEdgeData> garbageCollector)
		{
			Contract.Requires(world != null && garbageCollector != null);
			this.world = world;
			mapBase = world.Map.Base();
			this.garbageCollector = garbageCollector;
			garbageCollector.AttachTo(mapBase);
		}

		public void DoTurn()
		{
			AntContext[] contexts = SelectContext();
			RunTurn(contexts);
			garbageCollector.Collect();
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