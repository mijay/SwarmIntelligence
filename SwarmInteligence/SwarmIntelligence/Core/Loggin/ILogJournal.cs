using System;
using Common.Collections;
using SwarmIntelligence.Implementation.Logging;

namespace SwarmIntelligence.Core.Loggin
{
	public interface ILogJournal
	{
		ITailableCollection<LogRecord> Records { get; }
		event Action<long, long> OnRecordsAddedChunked;
		event Action<LogRecord[]> OnRecordsAdded;
	}
}