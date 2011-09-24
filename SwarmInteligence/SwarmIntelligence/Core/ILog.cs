namespace SwarmIntelligence.Core
{
	public interface ILog
	{
		void Log(string eventType, params object[] eventArgs);
	}
}