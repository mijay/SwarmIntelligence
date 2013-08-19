using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;

namespace SwarmIntelligence.Core.Loggin
{
	[ContractClass(typeof(ILogContract))]
	public interface ILog
	{
		void Log(string eventType, params object[] eventArgs);
	}
}