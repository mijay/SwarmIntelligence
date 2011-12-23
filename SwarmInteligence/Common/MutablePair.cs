namespace Common
{
	public class MutablePair<TKey, TValue>
	{
		public MutablePair(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		public TKey Key { get; private set; }
		public TValue Value { get; set; }
	}
}