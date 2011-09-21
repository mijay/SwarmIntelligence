using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Playground
{
	[ContractClass(typeof(IMapContract<,,>))]
	public interface IMap<TCoordinate, TNodeData, TEdgeData>
		: IEnumerable<KeyValuePair<TCoordinate, ICell<TCoordinate, TNodeData, TEdgeData>>>
	{
		[Pure]
		Topology<TCoordinate> Topology { get; }

		[Pure]
		bool Has(TCoordinate key);

		/// <remarks>
		///		Using of this method is preferable than using <see cref="Has"/> and then <see cref="IMutableMapping{TKey,TData}.Get"/>
		///		because it guaranties absence of race conditions.
		/// </remarks>
		[Pure]
		bool TryGet(TCoordinate coordinate, out ICell<TCoordinate, TNodeData, TEdgeData> cell);

		ICell<TCoordinate, TNodeData, TEdgeData> Get(TCoordinate coordinate);
	}
}