using System.Collections.Concurrent;
using SwarmIntelligence.Core;

namespace SwarmIntelligence.Infrastructure.Logging
{
	public class LogManager
	{
		private readonly ConcurrentQueue<TmpLogRecord> logAppendQueue = new ConcurrentQueue<TmpLogRecord>();

		public LogManager()
		{
			Log = new Logger(logAppendQueue);
		}

		public ILog Log { get; private set; }
	}
}