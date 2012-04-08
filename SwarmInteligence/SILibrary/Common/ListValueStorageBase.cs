using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Collections.Concurrent;
using Common.Collections.Extensions;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.Common
{
	public abstract class ListValueStorageBase<TKey, TValue>: IValueStorage<TKey, TValue>
		where TValue: class
	{
		private readonly ConcurrentList<TValue> values = new ConcurrentList<TValue>();

		#region IValueStorage<TKey,TValue> Members

		public bool TryGet(TKey key, out TValue value)
		{
			value = values[ToIndex(key)];
			return value != null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return values
				.Select((value, index) => value == null
				                          	? (KeyValuePair<TKey, TValue>?) null
				                          	: new KeyValuePair<TKey, TValue>(ToKey(index), value))
				.NotNull()
				.GetEnumerator();
		}

		public TValue GetOrCreate(TKey key, Func<TKey, TValue> value)
		{
			return values.GetOrCreate(ToIndex(key), () => value(key));
		}

		#endregion

		protected abstract int ToIndex(TKey key);
		protected abstract TKey ToKey(int index);
	}
}