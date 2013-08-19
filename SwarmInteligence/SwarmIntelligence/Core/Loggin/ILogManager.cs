namespace SwarmIntelligence.Core.Loggin
{
	public interface ILogManager
	{
		ILog Log { get; }
		ILogJournal Journal { get; }
	}
}