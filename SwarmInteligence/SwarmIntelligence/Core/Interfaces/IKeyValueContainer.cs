using System.Collections.Generic;

namespace SwarmIntelligence.Core.Interfaces
{
	/// <summary>
	/// Interface that represent storage of <typeparamref name="TValue"/>s assosiated with <typeparamref name="TKey"/>s.
	/// </summary>
	/// <typeparam name="TKey">Type of keys in mapping.</typeparam>
	/// <typeparam name="TValue">Type of values mapping.</typeparam>
	public interface IKeyValueContainer<TKey, TValue>: IEnumerable<KeyValuePair<TKey, TValue>>
	{
	}
}