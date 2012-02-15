using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;

namespace SwarmIntelligence.Core.Interfaces
{
	/// <summary>
	/// Class that represents sparsed mapping from keys of type <typeparamref name="TKey"/> to values of type <typeparamref name="TValue"/>.
	/// </summary>
	/// <typeparam name="TKey">Type of keys in mapping.</typeparam>
	/// <typeparam name="TValue">Type of values mapping.</typeparam>
	[ContractClass(typeof(ISparsedMappingContract<,>))]
	public interface ISparsedMapping<TKey, TValue>: IKeyValueContainer<TKey, TValue>
	{
		/// <summary>
		/// Checks does the <typeparamref name="TValue"/> associated with <paramref name="key"/> exist.
		/// And if true returns it.
		/// </summary>
		/// <param name="key">Key to check.</param>
		/// <param name="value"><typeparamref name="TValue"/> found in <see cref="ISparsedMapping{TKey,TValue}"/> at given key.</param>
		/// <exception cref="IndexOutOfRangeException"><paramref name="key"/> is illegal in current space.</exception>
		/// <returns><c>true</c> if <paramref name="value"/> was found. Otherwise - <c>false</c>.</returns>
		[Pure]
		bool TryGet(TKey key, out TValue value);
	}
}