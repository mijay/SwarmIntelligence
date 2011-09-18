using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
	public static class ObservableExtensions
	{
		public static IObservable<TSource> RepeatWhile<TSource>(this IObservable<TSource> source,
		                                                        Func<bool> condition)
		{
			Requires.NotNull(source, condition);
			return ProduceWhile(source, condition).Concat();
		}

		private static IEnumerable<IObservable<TSource>> ProduceWhile<TSource>(IObservable<TSource> source, Func<bool> condition)
		{
			Requires.NotNull(source, condition);
			do {
				yield return source;
			} while(condition());
		}
	}
}