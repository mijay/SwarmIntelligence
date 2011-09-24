namespace SwarmIntelligence.Infrastructure.Logging
{
	public struct LogRecord
	{
		public readonly long id;
		public readonly string type;
		public readonly object[] arguments;

		public LogRecord(long id, string type, object[] arguments)
			: this()
		{
			this.id = id;
			this.type = type;
			this.arguments = arguments;
		}
	}
}