using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Collections;
using SwarmIntelligence.Core.Loggin;

namespace SwarmIntelligence.Implementation.Logging
{
	internal class LogJournal: ILogJournal
	{
		private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
		private readonly List<Action<long, long>> onRecordsAddedChunkedDelegates = new List<Action<long, long>>();
		private readonly List<Action<LogRecord[]>> onRecordsAddedDelegates = new List<Action<LogRecord[]>>();

		public LogJournal(ITailableCollection<LogRecord> records)
		{
			Records = records;
		}

		public void NotifyRecordsAdded(LogRecord[] newRecords, long firstNewRecord, long lastNewRecord)
		{
			lockSlim.EnterReadLock();
			onRecordsAddedChunkedDelegates
				.AsParallel()
				.ForAll(x => x(firstNewRecord, lastNewRecord));
			onRecordsAddedDelegates
				.AsParallel()
				.ForAll(x => x(newRecords));
			lockSlim.ExitReadLock();
		}

		#region Implementation of ILogJournal

		public ITailableCollection<LogRecord> Records { get; private set; }

		public event Action<long, long> OnRecordsAddedChunked
		{
			add
			{
				lockSlim.EnterWriteLock();
				onRecordsAddedChunkedDelegates.Add(value);
				lockSlim.ExitWriteLock();
			}
			remove
			{
				lockSlim.EnterWriteLock();
				onRecordsAddedChunkedDelegates.Remove(value);
				lockSlim.ExitWriteLock();
			}
		}

		public event Action<LogRecord[]> OnRecordsAdded
		{
			add
			{
				lockSlim.EnterWriteLock();
				onRecordsAddedDelegates.Add(value);
				lockSlim.ExitWriteLock();
			}
			remove
			{
				lockSlim.EnterWriteLock();
				onRecordsAddedDelegates.Remove(value);
				lockSlim.ExitWriteLock();
			}
		}

		#endregion
	}
}