using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using Common.Collections.Extensions;

namespace Common.Collections.Concurrent
{
	public class ConcurrentLinkedList<T>: TailableCollectionBase<T>, IAppendableCollection<T>
	{
		private readonly ConcurrentLinkedListNode first;
		private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
		private long count;
		private ConcurrentLinkedListNode last;

		public ConcurrentLinkedList()
		{
			last = first = new ConcurrentLinkedListNode();
		}

		#region IAppendableCollection<T> Members

		public override T this[long index]
		{
			get
			{
				ConcurrentLinkedListNode result = first;
				for(int i = 0; i < index; i++)
					result = result.Next;
				return result.Value;
			}
		}

		public override long Count
		{
			get { return count; }
		}

		public void Add(T value)
		{
			lockSlim.EnterWriteLock();
			AddInternal(value);
			lockSlim.ExitWriteLock();
		}

		public void Add(IEnumerable<T> values)
		{
			lockSlim.EnterWriteLock();
			values.ForEach(AddInternal);
			lockSlim.ExitWriteLock();
		}

		public override IEnumerable<T> ReadFrom(long index)
		{
			ConcurrentLinkedListNode current = first;
			for(; index > 0; index--)
				current = current.Next;
			while(!current.IsLast) {
				yield return current.Value;
				current = current.Next;
			}
		}

		#endregion

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
	}
}