namespace SwarmIntelligence.Core
{
	public interface ILog
	{
		void Log<T>(T eventType, params object[] eventArgs)
			where T: struct;
	}
}