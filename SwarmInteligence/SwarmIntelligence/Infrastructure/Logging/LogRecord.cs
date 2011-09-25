using System.Diagnostics.Contracts;

namespace SwarmIntelligence.Infrastructure.Logging
{
	public struct LogRecord
	{
		public readonly object[] arguments;
		public readonly long id;
		public readonly string type;

		internal LogRecord(long id, TmpLogRecord tmpLogRecord)
			: this()
		{
			Contract.Requires(id >= 0 && tmpLogRecord.eventArgs != null && !string.IsNullOrEmpty(tmpLogRecord.eventType));
			this.id = id;
			type = tmpLogRecord.eventType;
			arguments = tmpLogRecord.eventArgs;
		}
	}
}