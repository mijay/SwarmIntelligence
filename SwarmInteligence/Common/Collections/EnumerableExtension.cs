using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Common.Collections
{
	public static class EnumerableExtension
	{
		public static bool IsEmpty<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);
			return source.Take(1).Count() == 0;
		}

		public static bool IsNotEmpty<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);
			return source.Take(1).Count() != 0;
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			Contract.Requires(source != null);
			Contract.Requires(action != null);
			foreach(T elem in source)
				action(elem);
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			Contract.Requires(source != null);
			Contract.Requires(action != null);
			int ind = 0;
			foreach(T elem in source) {
				action(elem, ind);
				ind++;
			}
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

		public static IEnumerable<TRes> SetMultiply<T1, T2, TRes>(this IEnumerable<T1> firstSet, IEnumerable<T2> secondSet,
		                                                          Func<T1, T2, TRes> mergeFunction)
		{
			Contract.Requires(firstSet != null && secondSet != null && mergeFunction != null);

			return firstSet.Join(secondSet, _ => 0, _ => 0, mergeFunction);
		}

		public static IEnumerable<T> NotNull<T>(this IEnumerable<T> enumerable) where T: class
		{
			Contract.Requires(enumerable != null);

			return enumerable.Where(x => x != null);
		}

		public static IEnumerable<T> Concat<T>(this IEnumerable<T> enumerable, T elem)
		{
			Contract.Requires(enumerable != null);

			return enumerable.Concat(new[] { elem });
		}

		public static bool IsAllUnique<T>(this IEnumerable<T> enumerable)
		{
			Contract.Requires(enumerable != null);
			return enumerable.GroupBy(x => x).Count(x => x.Count() != 1) == 0;
		}

		public static IEnumerable<KeyValuePair<T1, T2>> LazyZip<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right)
		{
			Contract.Requires(left != null && right != null);
			return left.Zip(right, (arg1, arg2) => new KeyValuePair<T1, T2>(arg1, arg2));
		}

		public static IEnumerable<KeyValuePair<T1, T2>> EagerZip<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right)
		{
			Contract.Requires(left != null && right != null);

			IEnumerator<T1> leftEnumerator = left.GetEnumerator();
			IEnumerator<T2> rightEnumerator = right.GetEnumerator();
			bool hasLeft, hasRight;

			while((hasLeft = leftEnumerator.MoveNext()) | (hasRight = rightEnumerator.MoveNext()))
				yield return new KeyValuePair<T1, T2>(
					hasLeft ? leftEnumerator.Current : default(T1),
					hasRight ? rightEnumerator.Current : default(T2));
		}
	}
}