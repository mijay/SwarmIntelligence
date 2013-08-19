using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Collections;
using Common.Collections.Concurrent;
using SwarmIntelligence.Core.Loggin;

namespace SwarmIntelligence.Implementation.Logging
{
	public class SimpleLogManager: ILogManager
	{
		public SimpleLogManager()
		{
			var journal = new LogJournalImpl();
			Log = new LogImpl(journal.Add);
			Journal = journal;
		}

		#region ILogManager Members

		public ILog Log { get; private set; }
		public ILogJournal Journal { get; private set; }

		#endregion

		#region Nested type: LogImpl

		private class LogImpl: ILog
		{
			private readonly Action<LogRecord> action;

			private int logIndex;

			#region Implementation of ILog

			public void Log(string eventType, params object[] eventArgs)
			{
				var logRecord = new LogRecord(Interlocked.Increment(ref logIndex), new TmpLogRecord(eventType, eventArgs));
				action(logRecord);
			}

			#endregion

			public LogImpl(Action<LogRecord> action)
			{
				this.action = action;
			}
		}

		#endregion

		#region Nested type: LogJournalImpl

		private class LogJournalImpl: ILogJournal
		{
			private readonly ChunkedArray<LogRecord> chunkedArray = new ChunkedArray<LogRecord>(10000);
			private readonly List<Action<LogRecord[]>> onRecordsAdded = new List<Action<LogRecord[]>>();
			private readonly List<Action<long, long>> onRecordsAddedChunked = new List<Action<long, long>>();

			public void Add(LogRecord logRecord)
			{
				long index = chunkedArray.Count;
				chunkedArray.Add(logRecord);
				var logRecords = new[] { logRecord };
				onRecordsAdded.ForEach(x => x(logRecords));
				onRecordsAddedChunked.ForEach(x => x(index, index));
			}

			#region Implementation of ILogJournal

			public ITailableCollection<LogRecord> Records
			{
				get { return chunkedArray; }
			}

			public event Action<long, long> OnRecordsAddedChunked
			{
				add
				{
					onRecordsAddedChunked.Add(value);
					value(0, chunkedArray.Count - 1);
				}
				remove { throw new NotImplementedException(); }
			}

			public event Action<LogRecord[]> OnRecordsAdded
			{
				add
				{
					onRecordsAdded.Add(value);
					value(chunkedArray.ToArray());
				}
				remove { throw new NotImplementedException(); }
			}

			#endregion
		}

		#endregion
	}
}