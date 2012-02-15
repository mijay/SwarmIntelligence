using System;
using Common.Collections;

namespace SwarmIntelligence.Implementation.Logging
{
	public interface ILogJournal
	{
		ITailableCollection<LogRecord> Records { get; }
		event Action<long, long> OnRecordsAddedChunked;
		event Action<LogRecord[]> OnRecordsAdded;
	}
}