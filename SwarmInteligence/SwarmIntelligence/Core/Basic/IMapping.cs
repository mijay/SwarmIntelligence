using System;

namespace SwarmIntelligence.Core.Basic
{
	public interface IMapping<in TKey, out TData>
	{
		/// <summary>
		/// TDOD
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="key"/> value is illegal or out of the bounds.</exception>
		TData Get(TKey key);
	}
}