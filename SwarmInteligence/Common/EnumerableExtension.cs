using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Common
{
    public static class EnumerableExtension
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            Contract.Requires(source != null);
            return source.Take(1).Count() == 0;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Contract.Requires(source != null);
            Contract.Requires(action != null);
            foreach(T elem in source)
                action(elem);
        }

        public static IEnumerable<T> Repeat<T>(this Func<T> func, int times)
        {
            Contract.Requires(func != null);
            Contract.Requires(times >= 0);

            return Enumerable.Repeat(0, times).Select(_ => func());
        }

        public static IDictionary<TKey, TVal2> MapValues<TKey, TVal1, TVal2>(this IDictionary<TKey, TVal1> dictionary,
                                                                                Func<TKey, TVal1, TVal2> func)
        {
            Contract.Requires(dictionary != null);
            Contract.Requires(func != null);
            return dictionary.ToDictionary(pair => pair.Key, pair => func(pair.Key, pair.Value));
        }

        public static IEnumerable<TRes> SetMultiply<T1, T2, TRes>(this IEnumerable<T1> firstSet, IEnumerable<T2> secondSet, Func<T1, T2, TRes> mergeFunction)
        {
            Contract.Requires(firstSet != null && secondSet != null && mergeFunction != null);

            return firstSet.Join(secondSet, _ => 0, _ => 0, mergeFunction);
        }
    }
}