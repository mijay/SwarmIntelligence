using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common.Concurrent;

namespace Common.Contracts
{
	[ContractClassFor(typeof(IAppendableCollection<>))]
	public abstract class IAppendableCollectionContract<T>: IAppendableCollection<T>
	{
		#region Implementation of IEnumerable

		public IEnumerator<T> GetEnumerator()
		{
			throw new UreachableCodeException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Implementation of IAppendableCollection<T>

		public T this[int index]
		{
			get
			{
				Contract.Requires(index >= 0 && index < Count);
				throw new UreachableCodeException();
			}
		}

		public int Count
		{
			get
			{
				Contract.Ensures(Contract.Result<int>() >= 0);
				throw new UreachableCodeException();
			}
		}

		public void Append(T value)
		{
			throw new UreachableCodeException();
		}

		public void Append(IEnumerable<T> values)
		{
			Contract.Requires(values != null);
			throw new UreachableCodeException();
		}

		public IEnumerable<T> ReadFrom(int index)
		{
			Contract.Requires(index >= 0 && index < Count);
			throw new UreachableCodeException();
		}

		#endregion
	}
}