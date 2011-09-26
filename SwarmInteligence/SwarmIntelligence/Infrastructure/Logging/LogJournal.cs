using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Collections;

namespace SwarmIntelligence.Infrastructure.Logging
{
	internal class LogJournal: ILogJournal
	{
		private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
		private readonly List<Action<long, long>> onRecordsAddedDelegates = new List<Action<long, long>>();

		public LogJournal(ITailableCollection<LogRecord> records)
		{
			Records = records;
		}

		public void NotifyRecordsAdded(long firstNewRecord, long lastNewRecord)
		{
			lockSlim.EnterReadLock();
			onRecordsAddedDelegates
				.AsParallel()
				.ForAll(x => x(firstNewRecord, lastNewRecord));
			lockSlim.ExitReadLock();
		}

		#region Implementation of ILogJournal

		public ITailableCollection<LogRecord> Records { get; private set; }

		public event Action<long, long> OnRecordsAdded
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