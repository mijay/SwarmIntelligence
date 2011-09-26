using System;
using Common.Collections;

namespace SwarmIntelligence.Infrastructure.Logging
{
	public interface ILogJournal
	{
		ITailableCollection<LogRecord> Records { get; }
		event Action<long, long> OnRecordsAdded;
	}
}