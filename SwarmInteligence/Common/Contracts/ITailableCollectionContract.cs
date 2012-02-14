using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common.Collections;

namespace Common.Contracts
{
	[ContractClassFor(typeof(ITailableCollection<>))]
	public abstract class ITailableCollectionContract<T>: ITailableCollection<T>
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

		public T this[long index]
		{
			get
			{
				Contract.Requires(index >= 0 && index < Count);
				throw new UreachableCodeException();
			}
		}

		public long Count
		{
			get
			{
				Contract.Ensures(Contract.Result<long>() >= 0);
				throw new UreachableCodeException();
			}
		}

		public IEnumerable<T> ReadFrom(long index)
		{
			Contract.Requires(index >= 0 && index < Count);
			throw new UreachableCodeException();
		}

		#endregion
	}
}