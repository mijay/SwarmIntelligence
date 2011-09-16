using System.Linq;
using Common.Collections;
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

		public Runner(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase)
		{
			this.mapBase = mapBase;
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
			contexts.ForEach(x => x.ant.ProcessTurn(x.coordinate, x.cell));
		}

		private AntContext[] SelectContext()
		{
			return mapBase
				.AsParallel()
				.SelectMany(x => x.Value.Select(ant => new AntContext { ant = ant.Base(), cell = x.Value.Base(), coordinate = x.Key }))
				.ToArray();
		}

		#region Nested type: AntContext

		private struct AntContext
		{
			public AntBase<TCoordinate, TNodeData, TEdgeData> ant;
			public CellBase<TCoordinate, TNodeData, TEdgeData> cell;
			public TCoordinate coordinate;
		}

		#endregion
	}
}