﻿using System;

namespace SwarmIntelligence.Core.Interfaces
{
	public interface ISparseMappint<in TKey, TData>: IMapping<TKey, TData>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="key"/> value is illegal or out of the bounds.</exception>
		bool Has(TKey key);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="key"/> value is illegal or out of the bounds.</exception>
		/// <remarks>
		///		Using of this method is preferable than using <see cref="Has"/> and then <see cref="IMapping{TKey,TData}.Get"/>
		///		because it guaranties absence of race conditions.
		/// </remarks>
		bool TryGet(TKey key, out TData data);
	}
}