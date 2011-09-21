using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Common.Collections
{
	public static class EnumerableExtension
	{
		[Pure]
		public static bool IsEmpty<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);
			return source.Take(1).Count() == 0;
		}

		[Pure]
		public static bool IsNotEmpty<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);
			return source.Take(1).Count() != 0;
		}

		[Pure]
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			Contract.Requires(source != null && action != null);
			foreach(T elem in source)
				action(elem);
		}

		[Pure]
		public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			Contract.Requires(source != null && action != null);
			int ind = 0;
			foreach(T elem in source) {
				action(elem, ind);
				ind++;
			}
		}

		[Pure]
		public static IEnumerable<T> Repeat<T>(this Func<T> func, int times)
		{
			Contract.Requires(func != null && times >= 0);
			return Enumerable.Repeat(0, times).Select(_ => func());
		}

		[Pure]
		public static IEnumerable<TRes> SetMultiply<T1, T2, TRes>(this IEnumerable<T1> firstSet, IEnumerable<T2> secondSet,
		                                                          Func<T1, T2, TRes> mergeFunction)
		{
			Contract.Requires(firstSet != null && secondSet != null && mergeFunction != null);
			return firstSet.Join(secondSet, _ => 0, _ => 0, mergeFunction);
		}

		[Pure]
		public static IEnumerable<T> NotNull<T>(this IEnumerable<T> source) where T: class
		{
			Contract.Requires(source != null);
			return source.Where(x => x != null);
		}

		[Pure]
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T elem)
		{
			Contract.Requires(source != null);
			return source.Concat(new[] { elem });
		}

		[Pure]
		public static bool IsAllUnique<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);
			return source.GroupBy(x => x).Count(x => x.Count() != 1) == 0;
		}

		[Pure]
		public static IEnumerable<KeyValuePair<T1, T2>> LazyZip<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right)
		{
			Contract.Requires(left != null && right != null);
			return left.Zip(right, (arg1, arg2) => new KeyValuePair<T1, T2>(arg1, arg2));
		}

		[Pure]
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