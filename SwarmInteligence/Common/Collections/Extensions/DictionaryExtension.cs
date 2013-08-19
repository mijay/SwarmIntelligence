using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Common.Collections.Extensions
{
	public static class DictionaryExtension
	{
		/// <summary>
		/// Add a value <paramref name="val"/> to the <paramref name="dictionary"/>
		/// with the key <paramref name="key"/> and return this dictionary.
		/// </summary>
		public static IDictionary<TKey, TVal> With<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key,
		                                                       TVal val)
		{
			Contract.Requires(dictionary != null);
			dictionary.Add(key, val);
			return dictionary;
		}

		/// <summary>
		/// Get value from <paramref name="dictionary"/> by the <paramref name="key"/>. If such value do not exist then return <paramref name="defaultVal"/>.
		/// </summary>
		[Pure]
		public static TVal GetOrDefault<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key,
		                                            TVal defaultVal = default(TVal))
		{
			Contract.Requires(dictionary != null);
			TVal result;
			if(!dictionary.TryGetValue(key, out result))
				result = defaultVal;
			return result;
		}

		public static TVal GetOrAdd<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key, Func<TVal> valueGetter)
		{
			Contract.Requires(dictionary != null && valueGetter != null);
			TVal result;
			if(!dictionary.TryGetValue(key, out result)) {
				result = valueGetter();
				dictionary.Add(key, result);
			}
			return result;
		}

		[Pure]
		public static IDictionary<TKey, TVal2> MapValues<TKey, TVal1, TVal2>(this IDictionary<TKey, TVal1> dictionary,
		                                                                     Func<TKey, TVal1, TVal2> func)
		{
			Contract.Requires(dictionary != null && func != null);
			return dictionary.ToDictionary(pair => pair.Key, pair => func(pair.Key, pair.Value));
		}

		public static TValue RemoveAndReturn<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			Contract.Requires(dictionary != null);
			Contract.Requires(dictionary.ContainsKey(key));
			TValue result = dictionary[key];
			dictionary.Remove(key);
			return result;
		}
	}
}