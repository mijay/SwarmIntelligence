using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common.Collections
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
            if (!dictionary.TryGetValue(key, out result)) {
                result = valueGetter();
                dictionary.Add(key ,result);
            }
            return result;
        }
    }
}