using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class MapBase<TCoordinate, TNodeData, TEdgeData>: IMap<TCoordinate, TNodeData, TEdgeData>
	{
		private readonly ICellProvider<TCoordinate, TNodeData, TEdgeData> cellProvider;

		protected MapBase(Topology<TCoordinate> topology, ICellProvider<TCoordinate, TNodeData, TEdgeData> cellProvider)
		{
			Contract.Requires(topology != null && cellProvider != null && cellProvider.Context == null);
			Topology = topology;
			this.cellProvider = cellProvider;
			cellProvider.Context = this;
		}

		#region IMap<TCoordinate,TNodeData,TEdgeData> Members

		public ICell<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate)
		{
			ICell<TCoordinate, TNodeData, TEdgeData> cell;
			if(TryGet(coordinate, out cell))
				return cell;

			CellBase<TCoordinate, TNodeData, TEdgeData> createdCell = cellProvider.Get(coordinate);
			cell = GetOrAdd(coordinate, createdCell);
			if(createdCell != cell)
				cellProvider.Return(createdCell);
			return cell;
		}

		public bool Has(TCoordinate key)
		{
			ICell<TCoordinate, TNodeData, TEdgeData> _;
			return TryGet(key, out _);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Topology<TCoordinate> Topology { get; private set; }

		#endregion

		#region Abstract methods

		public abstract bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell);
		public abstract IEnumerator<KeyValuePair<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>> GetEnumerator();
		protected abstract void Remove(TCoordinate coordinate);
		protected abstract ICell<TCoordinate, TNodeData, TEdgeData> GetOrAdd(TCoordinate coordinate, ICell<TCoordinate, TNodeData, TEdgeData> cell);

		#endregion

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
	}
}