using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Common.Collections.Concurrent
{
	public class ConcurrentSet<T>: IEnumerable<T>
	{
		private readonly ConcurrentDictionary<T, object> dictionary = new ConcurrentDictionary<T, object>();

		public bool IsEmpty
		{
			get { return dictionary.IsEmpty; }
		}

		public int Count
		{
			get { return dictionary.Count; }
		}

		public bool Add(T value)
		{
			return dictionary.TryAdd(value, new object());
		}

		public bool Remove(T value)
		{
			object _;
			return dictionary.TryRemove(value, out _);
		}

		public void Clear()
		{
			dictionary.Clear();
		}

		#region Implementation of IEnumerable

		public IEnumerator<T> GetEnumerator()
		{
			return dictionary.Keys.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}