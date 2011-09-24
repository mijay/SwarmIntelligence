namespace SwarmIntelligence.Infrastructure.Logging
{
	public struct LogRecord
	{
		public readonly long id;
		public readonly object[] arguments;

		public LogRecord(long id, object[] arguments)
			: this()
		{
			this.id = id;
			this.arguments = arguments;
		}
	}
}