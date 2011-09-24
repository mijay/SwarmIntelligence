using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using SwarmIntelligence.Core;

namespace SwarmIntelligence.Infrastructure.Logging
{
	public class Logger: ILog
	{
		private readonly ReaderWriterLockSlim @lock = new ReaderWriterLockSlim();

		private readonly List<LogRecord> log = new List<LogRecord>();
		private long recordId = -1;

		#region Implementation of ILog

		public void Log(string eventType, params object[] eventArgs)
		{
			Contract.Requires(eventArgs != null);
			@lock.EnterWriteLock();
			recordId++;
			log.Add(new LogRecord(recordId, eventType, eventArgs));
			@lock.ExitWriteLock();
		}

		#endregion
	}
}