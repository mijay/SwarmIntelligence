﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Concurrent
{
	public abstract class AppendableCollectionBase<T>: IAppendableCollection<T>
	{
		#region Implementation of IEnumerable

		public IEnumerator<T> GetEnumerator()
		{
			IEnumerable<T> result = Count == 0
			                        	? Enumerable.Empty<T>()
			                        	: ReadFrom(0);
			return result.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Implementation of IAppendableCollection<T>

		public abstract T this[long index] { get; }
		public abstract long Count { get; }
		public abstract void Append(T value);
		public abstract void Append(IEnumerable<T> values);
		public abstract IEnumerable<T> ReadFrom(long index);

		#endregion
	}
}