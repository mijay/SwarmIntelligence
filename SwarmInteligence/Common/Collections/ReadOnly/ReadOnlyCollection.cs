using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common.Collections.ReadOnly
{
	public class ReadOnlyCollection<T>: ICollection<T>
	{
		private readonly ICollection<T> innerCollection;

		public ReadOnlyCollection(ICollection<T> innerCollection)
		{
			Contract.Requires(innerCollection != null);
			this.innerCollection = innerCollection;
		}

		#region ICollection<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return innerCollection.GetEnumerator();
		}

		public void Add(T item)
		{
			throw new NotSupportedException("Collection is read-only and can not be modified");
		}

		public void Clear()
		{
			throw new NotSupportedException("Collection is read-only and can not be modified");
		}

		public bool Contains(T item)
		{
			return innerCollection.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			innerCollection.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			throw new NotSupportedException("Collection is read-only and can not be modified");
		}

		public int Count
		{
			get { return innerCollection.Count; }
		}

		public bool IsReadOnly
		{
			get { return true; }
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