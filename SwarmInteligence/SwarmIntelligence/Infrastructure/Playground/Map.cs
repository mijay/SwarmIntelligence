using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SwarmIntelligence.Infrastructure.Playground
{
	public class Map<TCoordinate, TNodeData, TEdgeData>: IMap<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> mapping;

		public Map(Topology<TCoordinate> topology, MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> mapping)
		{
			Contract.Requires(topology != null && mapping != null);
			Topology = topology;
			this.mapping = mapping;
		}

		#region IMap<TCoordinate,TNodeData,TEdgeData> Members

		public Topology<TCoordinate> Topology { get; private set; }

		public bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(coordinate));

			CellBase<TCoordinate, TNodeData, TEdgeData> cellBase;
			bool result = mapping.TryGet(coordinate, out cellBase);
			cell = cellBase;
			return result;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<KeyValuePair<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>> GetEnumerator()
		{
			return mapping.Select(x => new KeyValuePair<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>(x.Key, x.Value)).GetEnumerator();
		}

		#endregion

		#region Internal interface

		internal CellBase<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate)
		{
			Requires.True<IndexOutOfRangeException>(Topology.Lays(coordinate));

			return mapping.Get(coordinate);
		}

		#endregion
	}
}