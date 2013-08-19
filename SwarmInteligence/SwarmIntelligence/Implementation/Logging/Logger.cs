using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Core.Loggin;

namespace SwarmIntelligence.Implementation.Logging
{
	internal class Logger: ILog
	{
		private readonly ConcurrentQueue<TmpLogRecord> logAppendQueue;

		public Logger(ConcurrentQueue<TmpLogRecord> logAppendQueue)
		{
			Contract.Requires(logAppendQueue != null);
			this.logAppendQueue = logAppendQueue;
		}

		#region Implementation of ILog

		public void Log(string eventType, params object[] eventArgs)
		{
			logAppendQueue.Enqueue(new TmpLogRecord(eventType, eventArgs));
		}

		#endregion
	}
}