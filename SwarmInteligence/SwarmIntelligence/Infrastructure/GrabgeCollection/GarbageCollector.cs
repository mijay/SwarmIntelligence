using System.Linq;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence.Infrastructure.GrabgeCollection
{
	public class GarbageCollector<TCoordinate, TNodeData, TEdgeData>: IGarbageCollector<TCoordinate, TNodeData, TEdgeData>
	{
		#region Implementation of IGarbageCollector<TCoordinate,TNodeData,TEdgeData>

		public MapBase<TCoordinate, TNodeData, TEdgeData> MapBase { get; private set; }

		public void AttachTo(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase)
		{
			MapBase = mapBase;
		}

		public void Collect()
		{
			MapBase
				.AsParallel()
				.Select(cell => cell.Base())
				.Where(cellBase => cellBase.IsEmpty)
				.ForAll(cellBase => MapBase.Free(cellBase.Coordinate));
		}

		#endregion
	}
}