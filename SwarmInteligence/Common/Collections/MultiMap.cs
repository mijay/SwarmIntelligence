using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections.Extensions;

namespace Common.Collections
{
	public class MultiMap<TKey, TValue>: IEnumerable<IGrouping<TKey, TValue>>
	{
		private readonly Dictionary<TKey, List<TValue>> dictionary = new Dictionary<TKey, List<TValue>>();

		#region IEnumerable<IGrouping<TKey,TValue>> Members

		public IEnumerator<IGrouping<TKey, TValue>> GetEnumerator()
		{
			return dictionary
				.Select(x => (IGrouping<TKey, TValue>) new Grouping(x.Key, x.Value))
				.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		public void Add(TKey key, TValue value)
		{
			dictionary.GetOrAdd(key, () => new List<TValue>()).Add(value);
		}

		public void Remove(TKey key, TValue value)
		{
			if(dictionary.ContainsKey(key))
				dictionary[key].Remove(value);
		}

		public bool RemoveFirst(TKey key, out TValue removedValue)
		{
			if(dictionary.ContainsKey(key)) {
				List<TValue> list = dictionary[key];
				if(list.IsNotEmpty()) {
					removedValue = list.First();
					list.RemoveAt(0);
					return true;
				}
			}
			removedValue = default(TValue);
			return false;
		}

		public IEnumerable<TValue> this[TKey key]
		{
			get
			{
				if (!dictionary.ContainsKey(key))
					return Enumerable.Empty<TValue>();
				return dictionary[key];
			}
		}

		public bool ContainsKey(TKey key)
		{
			return dictionary.ContainsKey(key);
		}

		#region Nested type: Grouping

		private class Grouping: IGrouping<TKey, TValue>
		{
			private readonly IEnumerable<TValue> values;

			public Grouping(TKey key, IEnumerable<TValue> values)
			{
				Contract.Requires(values != null);
				this.values = values;
				Key = key;
			}

			#region IGrouping<TKey,TValue> Members

			public IEnumerator<TValue> GetEnumerator()
			{
				return values.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public TKey Key { get; private set; }

			#endregion
		}

		#endregion
	}
}