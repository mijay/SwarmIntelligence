using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using Common.Collections.Extensions;

namespace Common.Collections.Concurrent
{
	public class ConcurrentList<T>: IAppendableCollection<T>
	{
		private readonly List<T> list = new List<T>();
		private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		#region Implementation of IEnumerable

		public IEnumerator<T> GetEnumerator()
		{
			lockSlim.EnterReadLock();
			try {
				return list.ToArray().AsEnumerable().GetEnumerator();
			}
			finally {
				lockSlim.ExitReadLock();
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Implementation of IAppendableCollection<T>

		public void Add(T value)
		{
			lockSlim.EnterWriteLock();
			try {
				list.Add(value);
			}
			finally {
				lockSlim.ExitWriteLock();
			}
		}

		public void Add(IEnumerable<T> values)
		{
			Contract.Requires(values != null);
			lockSlim.EnterWriteLock();
			try {
				list.AddRange(values);
			}
			finally {
				lockSlim.ExitWriteLock();
			}
		}

		#endregion

		public int Count
		{
			get
			{
				Contract.Ensures(Contract.Result<int>() >= 0);
				return list.Count;
			}
		}

		public T this[int index]
		{
			get
			{
				Contract.Requires(index >= 0);

				if(index >= list.Count)
					return default(T);

				lockSlim.EnterReadLock();
				try {
					return list[index];
				}
				finally {
					lockSlim.ExitReadLock();
				}
			}
			set
			{
				Contract.Requires(index >= 0);

				if(list.Count > index) {
					lockSlim.EnterReadLock();
					try {
						list[index] = value;
					}
					finally {
						lockSlim.ExitReadLock();
					}
				} else {
					lockSlim.EnterWriteLock();
					try {
						if(list.Count > index)
							list.AddRange(Enumerable.Repeat(default(T), index - list.Count).Concat(value));
						else
							list[index] = value;
					}
					finally {
						lockSlim.ExitWriteLock();
					}
				}
			}
		}

		public T CompareExchange(int index, T value, T comparand)
		{
			Contract.Requires(index >= 0);
			lockSlim.EnterWriteLock();
			try {
				T current = this[index];
				if(ReferenceEquals(current, null) ? ReferenceEquals(comparand, null) : current.Equals(comparand))
					this[index] = value;
				return current;
			}
			finally {
				lockSlim.ExitWriteLock();
			}
		}
	}
}