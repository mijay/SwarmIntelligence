using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

		public Topology<TCoordinate> Topology { get; private set; }
		public abstract bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell);
		public abstract IEnumerator<ICell<TCoordinate, TNodeData, TEdgeData>> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Internal interface

		internal ICell<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate)
		{
			Contract.Requires(Topology.Lays(coordinate));

			ICell<TCoordinate, TNodeData, TEdgeData> cell;
			if(TryGet(coordinate, out cell))
				return cell;

			CellBase<TCoordinate, TNodeData, TEdgeData> createdCell = cellProvider.Get(coordinate);
			cell = GetOrAdd(coordinate, createdCell);
			if(createdCell != cell)
				cellProvider.Return(createdCell);
			return cell;
		}

		internal void Free(TCoordinate coordinate)
		{
			Contract.Requires(Topology.Lays(coordinate));

			ICell<TCoordinate, TNodeData, TEdgeData> cell;
			if(!TryGet(coordinate, out cell))
				return;
			Remove(coordinate);
			cellProvider.Return(cell.Base());
		}

		#endregion

		#region Abstract methods

		protected abstract void Remove(TCoordinate coordinate);
		protected abstract ICell<TCoordinate, TNodeData, TEdgeData> GetOrAdd(TCoordinate coordinate, ICell<TCoordinate, TNodeData, TEdgeData> cell);

		#endregion
	}
}