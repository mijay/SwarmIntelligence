using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Common.Concurrent
{
	public class ConcurrentSet<T>: IEnumerable<T>
	{
		private readonly ConcurrentDictionary<T, object> dictionary = new ConcurrentDictionary<T, object>();

		public bool Add(T value)
		{
			return dictionary.TryAdd(value, new object());
		}

		public bool Remove(T value)
		{
			object _;
			return dictionary.TryRemove(value, out _);
		}

		public void Clean()
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