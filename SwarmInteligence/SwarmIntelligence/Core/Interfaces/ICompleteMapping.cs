using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Contracts;

namespace SwarmIntelligence.Core.Interfaces
{
	/// <summary>
	/// Class that represents complete mapping from keys of type <typeparamref name="TKey"/> to values of type <typeparamref name="TValue"/>.
	/// </summary>
	/// <typeparam name="TKey">Type of keys in mapping.</typeparam>
	/// <typeparam name="TValue">Type of values mapping.</typeparam>
	[ContractClass(typeof(ICompleteMappingContract<,>))]
	public interface ICompleteMapping<in TKey, TValue>
	{
		/// <summary>
		/// Returns the <typeparamref name="TValue"/> associated with the <paramref name="key"/>.
		/// </summary>
		/// <param name="key">Key to check.</param>
		/// <exception cref="IndexOutOfRangeException"><paramref name="key"/> is illegal in current space.</exception>
		/// <returns><typeparamref name="TValue"/> associated with the <paramref name="key"/>.</returns>
		[Pure]
		TValue Get(TKey key);

		/// <summary>
		/// Sets the <typeparamref name="TValue" /> associated with the <paramref name="key" /> to <paramref name="value" />.
		/// Override existing one.
		/// </summary>
		/// <exception cref="NotSupportedException">This method is not supported.</exception>
		void Set(TKey key, TValue value);
	}
}