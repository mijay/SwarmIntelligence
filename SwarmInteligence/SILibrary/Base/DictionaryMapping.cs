using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SwarmIntelligence.Core;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.Base
{
	public class DictionaryMapping<TKey, TValue>: MappingBase<TKey, TValue>
	{
		private readonly ConcurrentDictionary<TKey, TValue> dictionary = new ConcurrentDictionary<TKey, TValue>();

		public DictionaryMapping(IValueProvider<TKey, TValue> valueProvider, ILog log)
			: base(valueProvider, log)
		{
		}

		#region Overrides of Map<TCoordinate,TNodeData,TEdgeData>

		public override bool TryGet(TKey key, out TValue value)
		{
			return dictionary.TryGetValue(key, out value);
		}

		public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}

		protected override bool TryRemove(TKey key, out TValue value)
		{
			return dictionary.TryRemove(key, out value);
		}

		protected override TValue GetOrAdd(TKey key, TValue value)
		{
			return dictionary.GetOrAdd(key, value);
		}

		#endregion
	}
}