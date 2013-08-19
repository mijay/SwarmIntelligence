using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Collections.Concurrent;
using Common.Tasks;
using SwarmIntelligence.Core.Loggin;

namespace SwarmIntelligence.Implementation.Logging
{
	public class LogManager: ILogManager
	{
		private readonly ConcurrentQueue<TmpLogRecord> logAppendQueue = new ConcurrentQueue<TmpLogRecord>();
		private readonly LogJournal logJournal;
		private readonly ChunkedArray<LogRecord> logRecords = new ChunkedArray<LogRecord>(10 * 1024);
		private long logRecordIndex = -1;

		public LogManager()
		{
			Log = new Logger(logAppendQueue);
			logJournal = new LogJournal(logRecords);

			Task.Factory
				.StartNewDelayed(300)
				.Then(() => LaunchLogProcessing());
		}

		#region ILogManager Members

		public ILog Log { get; private set; }

		public ILogJournal Journal
		{
			get { return logJournal; }
		}

		#endregion

		private Task LaunchLogProcessing()
		{
			ProcessLog();
			return Task.Factory
				.StartNewDelayed(100)
				.Then(() => LaunchLogProcessing());
		}

		private void ProcessLog()
		{
			if(logAppendQueue.IsEmpty)
				return;

			long lastOldRecord = logRecordIndex;
			LogRecord[] newRecords = DequeueAllAppended();
			long lastNewRecord = logRecordIndex;

			logRecords.Add(newRecords);

			Task.Factory.StartNew(
				() => logJournal.NotifyRecordsAdded(newRecords, lastOldRecord + 1, lastNewRecord),
				TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness);
		}

		private LogRecord[] DequeueAllAppended()
		{
			var newRecords = new List<LogRecord>(logAppendQueue.Count);
			TmpLogRecord tmpLogRecord;
			while(logAppendQueue.TryDequeue(out tmpLogRecord))
				newRecords.Add(new LogRecord(++logRecordIndex, tmpLogRecord));
			return newRecords.ToArray();
		}
	}
}