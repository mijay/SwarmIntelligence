using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(ILog))]
	public abstract class ILogContract: ILog
	{
		#region Implementation of ILog

		public void Log(string eventType, params object[] eventArgs)
		{
			Contract.Requires(!string.IsNullOrEmpty(eventType) && eventArgs != null);
			throw new UnreachableCodeException();
		}

		#endregion
	}
}