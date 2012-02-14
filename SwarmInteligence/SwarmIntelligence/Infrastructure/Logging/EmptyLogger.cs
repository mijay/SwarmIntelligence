using SwarmIntelligence.Core;

namespace SwarmIntelligence.Infrastructure.Logging
{
	public class EmptyLogger: ILog
	{
		static EmptyLogger()
		{
			Instanse = new EmptyLogger();
		}

		public static ILog Instanse { get; private set; }

		#region Implementation of ILog

		public void Log(string eventType, params object[] eventArgs)
		{
		}

		#endregion
	}
}