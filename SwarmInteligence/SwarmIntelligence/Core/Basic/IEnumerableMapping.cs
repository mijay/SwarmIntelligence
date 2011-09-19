using System.Collections.Generic;

namespace SwarmIntelligence.Core.Basic
{
	public interface IEnumerableMapping<TKey, TData>: IMapping<TKey, TData>, IEnumerable<KeyValuePair<TKey, TData>>
	{
	}
}