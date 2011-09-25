using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using Common.Collections;

namespace Common.Concurrent
{
	public class ChunkedArray<T>: AppendableCollectionBase<T>
	{
		private readonly ConcurrentLinkedList<T[]> chunkList
			= new ConcurrentLinkedList<T[]>();

		private readonly int chunkSize;
		private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
		private int count;

		public ChunkedArray(int chunkSize)
		{
			Contract.Requires(chunkSize > 0);
			this.chunkSize = chunkSize;
			chunkList.Append(new T[chunkSize]);
		}

		public override int Count
		{
			get { return count; }
		}

		public override T this[int index]
		{
			get
			{
				int chunkNumber = index / chunkSize;
				int indexInChunk = index % chunkSize;
				return chunkList[chunkNumber][indexInChunk];
			}
		}

		public override void Append(IEnumerable<T> values)
		{
			lockSlim.EnterWriteLock();
			values.ForEach(AppendInternal);
			lockSlim.ExitWriteLock();
		}

		public override IEnumerable<T> ReadFrom(int index)
		{
			int chunkNumber = index / chunkSize;
			IEnumerator<T[]> chunks = chunkList.ReadFrom(chunkNumber).GetEnumerator();
			T[] arrayChunk = null;
			while(index < count) {
				int numberInChunk = index % chunkSize;
				if(numberInChunk == 0 || arrayChunk == null) {
					chunks.MoveNext();
					arrayChunk = chunks.Current;
				}
				yield return arrayChunk[numberInChunk];

				index++;
			}
		}

		public override void Append(T value)
		{
			lockSlim.EnterWriteLock();
			AppendInternal(value);
			lockSlim.ExitWriteLock();
		}

		private void AppendInternal(T data)
		{
			int chunkNumber = count / chunkSize;
			int indexInChunk = count % chunkSize;
			chunkList[chunkNumber][indexInChunk] = data;
			count++;
			if(indexInChunk == chunkSize - 1)
				chunkList.Append(new T[chunkSize]);
		}
	}
}