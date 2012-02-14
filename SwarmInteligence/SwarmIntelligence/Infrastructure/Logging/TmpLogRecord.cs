using System.Diagnostics.Contracts;

namespace SwarmIntelligence.Infrastructure.Logging
{
	internal struct TmpLogRecord
	{
		public readonly object[] eventArgs;
		public readonly string eventType;

		public TmpLogRecord(string eventType, object[] eventArgs)
		{
			Contract.Requires(!string.IsNullOrEmpty(eventType) && eventArgs != null);
			this.eventType = eventType;
			this.eventArgs = eventArgs;
		}
	}
}