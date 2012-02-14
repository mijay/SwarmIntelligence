using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common.Collections.ReadOnly
{
	public class ReadOnlyList<T>: IList<T>
	{
		private readonly IList<T> innerList;

		public ReadOnlyList(IList<T> innerList)
		{
			Contract.Requires(innerList != null);
			this.innerList = innerList;
		}

		#region IList<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return innerList.GetEnumerator();
		}

		public void Add(T item)
		{
			throw new NotSupportedException("List is read-only and can not be modified");
		}

		public void Clear()
		{
			throw new NotSupportedException("List is read-only and can not be modified");
		}

		public bool Contains(T item)
		{
			return innerList.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			innerList.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			throw new NotSupportedException("List is read-only and can not be modified");
		}

		public int Count
		{
			get { return innerList.Count; }
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public int IndexOf(T item)
		{
			return innerList.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			throw new NotSupportedException("List is read-only and can not be modified");
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException("List is read-only and can not be modified");
		}

		public T this[int index]
		{
			get { return innerList[index]; }
			set { throw new NotSupportedException("List is read-only and can not be modified"); }
		}

		#endregion

		#region Implementation of IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}