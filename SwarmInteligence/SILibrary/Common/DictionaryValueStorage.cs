using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.Common
{
	public class DictionaryValueStorage<TKey, TValue>: IValueStorage<TKey, TValue>
	{
		private readonly ConcurrentDictionary<TKey, TValue> dictionary = new ConcurrentDictionary<TKey, TValue>();

		#region IValueStorage<TKey,TValue> Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dictionary.ToArray().AsEnumerable().GetEnumerator();
		}

		public bool TryGet(TKey key, out TValue value)
		{
			return dictionary.TryGetValue(key, out value);
		}

		public TValue GetOrCreate(TKey key, Func<TKey, TValue> valueFactory)
		{
			return dictionary.GetOrAdd(key, valueFactory);
		}

		#endregion
	}
}