﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Concurrent;
using SwarmIntelligence.Core;

namespace SwarmIntelligence.Infrastructure.Logging
{
	public class LogManager
	{
		private readonly ConcurrentQueue<TmpLogRecord> logAppendQueue = new ConcurrentQueue<TmpLogRecord>();
		private readonly ChunkedArray<LogRecord> logRecords = new ChunkedArray<LogRecord>(10 * 1024);
		private long logRecordIndex = -1;

		public LogManager()
		{
			Log = new Logger(logAppendQueue);
		}

		public ILog Log { get; private set; }

		private void ProcessLog()
		{
			if(logAppendQueue.IsEmpty)
				return;

			//var firstExclusiveIndex = logRecordIndex;
			IEnumerable<LogRecord> newRecords = DequeueAllAppended();
			//var lastInclusiveIndex = logRecordIndex;

			logRecords.Append(newRecords);
		}

		private IEnumerable<LogRecord> DequeueAllAppended()
		{
			var newRecords = new List<LogRecord>(logAppendQueue.Count);
			TmpLogRecord tmpLogRecord;
			while(logAppendQueue.TryDequeue(out tmpLogRecord))
				newRecords.Add(new LogRecord(++logRecordIndex, tmpLogRecord));
			return newRecords;
		}
	}
}