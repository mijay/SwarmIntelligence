﻿using System;
using Common.Cache;

namespace Common.Memoization
{
	public class Memoizer: IMemoizer
	{
		private readonly IKeyValueCache cache;

		public Memoizer(IKeyValueCache cache)
		{
			this.cache = cache;
		}

		#region Implementation of IMemoizer

		public Func<TVal> Memoize<TVal>(Func<TVal> func)
		{
			return () => cache.GetOrAdd(Tuple.Create(func), tuple => tuple.Item1());
		}

		public Func<TArg, TVal> Memoize<TArg, TVal>(Func<TArg, TVal> func)
		{
			return arg => cache.GetOrAdd(Tuple.Create(func, arg), tuple => tuple.Item1(tuple.Item2));
		}

		public Func<TArg1, TArg2, TVal> Memoize<TArg1, TArg2, TVal>(Func<TArg1, TArg2, TVal> func)
		{
			return (arg1, arg2) => cache.GetOrAdd(Tuple.Create(func, arg1, arg2), tuple => tuple.Item1(tuple.Item2, tuple.Item3));
		}

		#endregion
	}
}