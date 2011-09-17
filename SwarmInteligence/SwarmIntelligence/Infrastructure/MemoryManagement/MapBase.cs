using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class MapBase<TCoordinate, TNodeData, TEdgeData>: Map<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly ICellProvider<TCoordinate, TNodeData, TEdgeData> cellProvider;

		protected MapBase(Topology<TCoordinate> topology, ICellProvider<TCoordinate, TNodeData, TEdgeData> cellProvider)
			: base(topology)
		{
			Contract.Requires(cellProvider != null);

			this.cellProvider = cellProvider;
		}

		public virtual void OnTurnBegin()
		{
		}

		public virtual void OnTurnEnd()
		{
			this
				.Select(x => x.Value)
				.Select(x => x.Base())
				.Where(x => x.IsEmpty)
				.ForEach(x => {
				         	Remove(x.Coordinate);
				         	cellProvider.Return(x);
				         });
		}

		public override Cell<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate key)
		{
			Cell<TCoordinate, TNodeData, TEdgeData> cell;
			if(TryGet(key, out cell))
				return cell;

			CellBase<TCoordinate, TNodeData, TEdgeData> createdCell = cellProvider.Get(key);
			cell = GetOrAdd(key, createdCell);
			if(createdCell != cell)
				cellProvider.Return(createdCell);
			return cell;
		}

		protected abstract void Remove(TCoordinate coordinate);
		protected abstract Cell<TCoordinate, TNodeData, TEdgeData> GetOrAdd(TCoordinate coordinate, Cell<TCoordinate, TNodeData, TEdgeData> cell);
	}
}