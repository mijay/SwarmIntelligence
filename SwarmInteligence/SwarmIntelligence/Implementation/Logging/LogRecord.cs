using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections.Extensions;

namespace SwarmIntelligence.Implementation.Logging
{
	[DebuggerDisplay("{type} with [{GetDebugView()}]")]
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

		private string GetDebugView()
		{
			return arguments.Select(x => "{" + x.ToString() + "}").JoinStrings(", ");
		}
	}
}