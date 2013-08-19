using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using Common.Collections.Extensions;

namespace Common.Collections.Concurrent
{
	public class ConcurrentList<T>: IAppendableCollection<T>
		where T: class
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

				lockSlim.EnterReadLock();
				try {
					return index >= list.Count ? null : list[index];
				}
				finally {
					lockSlim.ExitReadLock();
				}
			}
			set
			{
				Contract.Requires(index >= 0);

				lockSlim.EnterUpgradeableReadLock();

				try {
					if(list.Count > index)
						list[index] = value;
					else {
						lockSlim.EnterWriteLock();
						try {
							list.AddRange(Enumerable.Repeat(default(T), index - list.Count).Concat(value));
						}
						finally {
							lockSlim.ExitWriteLock();
						}
					}
				}
				finally {
					lockSlim.ExitUpgradeableReadLock();
				}
			}
		}

		public T GetOrCreate(int index, Func<T> valueBuilder)
		{
			Contract.Requires(index >= 0 && valueBuilder != null);
			Contract.Ensures(Contract.Result<T>() != null);

			lockSlim.EnterUpgradeableReadLock();
			try {
				T result;

				if(list.Count <= index)
					this[index] = result = valueBuilder();
				else {
					result = list[index];
					if(result == null)
						list[index] = result = valueBuilder();
				}
				return result;
			}
			finally {
				lockSlim.ExitUpgradeableReadLock();
			}
		}
	}
}