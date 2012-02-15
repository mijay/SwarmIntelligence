namespace SwarmIntelligence.MemoryManagement
{
	public interface IValueProvider<in TKey, TValue>
	{
		void Return(TValue cell);
		TValue Get(TKey key);
	}
}