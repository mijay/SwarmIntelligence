namespace SwarmIntelligence.Core
{
	public interface ILog
	{
		void Log(object eventType);
		void Log(object eventType, object eventArg);
		void Log(object eventType, object eventArg1, object eventArg2);
		void Log(object eventType, object eventArg1, object eventArg2, object eventArg3);
		void Log(object eventType, params object[] eventArgs);
	}
}