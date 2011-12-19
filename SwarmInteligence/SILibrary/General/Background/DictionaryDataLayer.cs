using System.Collections.Concurrent;
using SwarmIntelligence.Core;

namespace SILibrary.General.Background
{
	public class DictionaryDataLayer<TKey, TValue>: DataLayer<TKey, TValue>
	{
		private readonly ConcurrentDictionary<TKey, TValue> dictionary = new ConcurrentDictionary<TKey, TValue>();

		public override TValue Get(TKey key)
		{
			return dictionary[key];
		}

		public override void Set(TKey key, TValue data)
		{
			dictionary[key] = data;
		}
	}
}