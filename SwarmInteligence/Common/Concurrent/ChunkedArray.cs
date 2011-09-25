using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;

namespace Common.Concurrent
{
	public class ChunkedArray<T>: IEnumerable<T>
	{
		private readonly ConcurrentLinkedList<ArrayChunk> chunkList
			= new ConcurrentLinkedList<ArrayChunk>();

		private readonly int chunkSize;
		private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
		private int length;

		public ChunkedArray(int chunkSize)
		{
			this.chunkSize = chunkSize;
			Contract.Requires(chunkSize > 0);
			chunkList.Add(new ArrayChunk(chunkSize));
		}

		public int Length
		{
			get { return length; }
		}

		public T this[int index]
		{
			get
			{
				Contract.Requires(index > 0 && index < Length);
				int chunkNumber = index / chunkSize;
				int indexInChunk = index % chunkSize;
				return chunkList[chunkNumber].Data[indexInChunk];
			}
		}

		public void Add(T data)
		{
			lockSlim.EnterWriteLock();
			int chunkNumber = length / chunkSize;
			int indexInChunk = length % chunkSize;
			chunkList[chunkNumber].Data[indexInChunk] = data;
			length++;
			if(indexInChunk == chunkSize - 1)
				chunkList.Add(new ArrayChunk(chunkSize));
			lockSlim.ExitWriteLock();
		}

		#region Nested type: ArrayChunk

		private class ArrayChunk
		{
			public ArrayChunk(int dataSize)
			{
				Data = new T[dataSize];
			}

			public int Length { get; set; }
			public T[] Data { get; private set; }
		}

		#endregion

		#region Implementation of IEnumerable

		public IEnumerator<T> GetEnumerator()
		{
			int index = 0;
			IEnumerator<ArrayChunk> chunks = chunkList.GetEnumerator();
			ArrayChunk arrayChunk = null;
			while(index < length) {
				int numberInChunk = index % chunkSize;
				if(numberInChunk == 0) {
					chunks.MoveNext();
					arrayChunk = chunks.Current;
				}
				yield return arrayChunk.Data[numberInChunk];
				index++;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}