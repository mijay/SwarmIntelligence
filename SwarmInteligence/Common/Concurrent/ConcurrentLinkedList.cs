using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using Common.Collections;

namespace Common.Concurrent
{
	public class ConcurrentLinkedList<T>: IEnumerable<T>
	{
		private readonly ConcurrentLinkedListNode first;
		private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
		private int count;
		private ConcurrentLinkedListNode last;

		public ConcurrentLinkedList()
		{
			last = first = new ConcurrentLinkedListNode();
		}

		public T this[int index]
		{
			get
			{
				Contract.Requires(index >= 0 && index < Count);
				ConcurrentLinkedListNode result = first;
				for(int i = 0; i < index; i++)
					result = result.Next;
				return result.Value;
			}
		}

		public int Count
		{
			get { return count; }
		}

		public void Add(T value)
		{
			lockSlim.EnterWriteLock();
			AddInternal(value);
			lockSlim.ExitWriteLock();
		}

		public void AddRange(IEnumerable<T> values)
		{
			Contract.Requires(values != null);
			lockSlim.EnterWriteLock();
			values.ForEach(AddInternal);
			lockSlim.ExitWriteLock();
		}

		private void AddInternal(T value)
		{
			var newNode = new ConcurrentLinkedListNode();
			last.AddAfter(value, newNode);
			last = newNode;
			count++;
		}

		#region Nested type: ConcurrentLinkedListNode

		private class ConcurrentLinkedListNode
		{
			public ConcurrentLinkedListNode Next { get; private set; }
			public T Value { get; private set; }

			public bool IsLast
			{
				get { return Next == null; }
			}

			public void AddAfter(T valueOfCurrent, ConcurrentLinkedListNode next)
			{
				Contract.Requires(IsLast && next != null && next.IsLast);

				Value = valueOfCurrent;
				Next = next;
			}
		}

		#endregion

		#region Implementation of IEnumerable

		public IEnumerator<T> GetEnumerator()
		{
			ConcurrentLinkedListNode current = first;
			while(!current.IsLast) {
				yield return current.Value;
				current = current.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}