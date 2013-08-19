using System.Collections.Generic;
using Common.Collections.Concurrent;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Playground;

namespace SILibrary.Common
{
	public class SetCell<TCoordinate, TNodeData, TEdgeData>: CellBase<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly ConcurrentSet<IAnt<TCoordinate, TNodeData, TEdgeData>> set =
			new ConcurrentSet<IAnt<TCoordinate, TNodeData, TEdgeData>>();

		public SetCell(Map<TCoordinate, TNodeData, TEdgeData> map, TCoordinate coordinate)
			: base(map, coordinate)
		{
		}

		#region Overrides of Cell<TCoordinate,TNodeData,TEdgeData>

		public override IEnumerator<IAnt<TCoordinate, TNodeData, TEdgeData>> GetEnumerator()
		{
			return set.GetEnumerator();
		}

		#endregion

		#region Overrides of CellBase<TCoordinate,TNodeData,TEdgeData>

		public override bool IsEmpty
		{
			get { return set.IsEmpty; }
		}

		public override void Add(IAnt<TCoordinate, TNodeData, TEdgeData> ant)
		{
			set.Add(ant);
		}

		public override void Remove(IAnt<TCoordinate, TNodeData, TEdgeData> ant)
		{
			set.Remove(ant);
		}

		#endregion
	}
}