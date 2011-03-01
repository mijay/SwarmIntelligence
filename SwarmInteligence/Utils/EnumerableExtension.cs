using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Utils
{
    public static class EnumerableExtension
    {
        public static bool AreDistinct<T>(this IEnumerable<T> source)
        {
            Contract.Requires(source != null);
            return source.Distinct().Count() == source.Count();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            Contract.Requires(source != null);
            using(IEnumerator<T> enumerator = source.GetEnumerator())
                return !enumerator.MoveNext();
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

        public static void InvokeTimes(this Action func, int times)
        {
            Contract.Requires(func != null);
            Contract.Requires(times >= 0);

            Enumerable.Repeat(0, times).ForEach(_ => func());
        }

        public static IDictionary<TKey, TVal2> SelectValues<TKey, TVal1, TVal2>(this IDictionary<TKey, TVal1> dictionary,
                                                                                Func<TKey, TVal1, TVal2> func)
        {
            Contract.Requires(dictionary != null);
            Contract.Requires(func != null);
            return dictionary.ToDictionary(pair => pair.Key, pair => func(pair.Key, pair.Value));
        }
    }
}