using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	public abstract class Map<TCoordinate, TNodeData, TEdgeData>:
		ISparseMappint<TCoordinate, Cell<TCoordinate, TNodeData, TEdgeData>>,
		IEnumerableMapping<TCoordinate, Cell<TCoordinate, TNodeData, TEdgeData>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		protected Map(Topology<TCoordinate> topology)
		{
			Contract.Requires(topology != null);

			Topology = topology;
		}

		[Pure]
		public Topology<TCoordinate> Topology { get; private set; }

		#region Implementation of IMapping<in TCoordinate,out Cell<TCoordinate,TNodeData,TEdgeData>>

		public abstract Cell<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate key);

		#endregion

		#region Implementation of ISparseMappint<in TCoordinate,Cell<TCoordinate,TNodeData,TEdgeData>>

		[Pure]
		public virtual bool Has(TCoordinate key)
		{
			Cell<TCoordinate, TNodeData, TEdgeData> cell;
			return TryGet(key, out cell);
		}

		[Pure]
		public abstract bool TryGet(TCoordinate key, out Cell<TCoordinate, TNodeData, TEdgeData> data);

		#endregion

		#region Implementation of IEnumerable

		public abstract IEnumerator<KeyValuePair<TCoordinate, Cell<TCoordinate, TNodeData, TEdgeData>>> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}