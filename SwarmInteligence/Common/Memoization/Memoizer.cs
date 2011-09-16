using System;
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

		public IMemoizedFunc<TVal> Memoize<TVal>(Func<TVal> func)
		{
			return new MemoizedFunc<TVal>(cache, func);
		}

		public IMemoizedFunc<TArg, TVal> Memoize<TArg, TVal>(Func<TArg, TVal> func)
		{
			return new MemoizedFunc<TArg, TVal>(cache, func);
		}

		#endregion

		#region Nested type: MemoizedFunc

		private class MemoizedFunc<TVal>: IMemoizedFunc<TVal>
		{
			private readonly Func<TVal> builder;
			private readonly IKeyValueCache keyValueCache;

			public MemoizedFunc(IKeyValueCache keyValueCache, Func<TVal> builder)
			{
				this.keyValueCache = keyValueCache;
				this.builder = builder;
			}

			#region Implementation of IMemoizedFunc<TVal>

			public TVal Get()
			{
				return keyValueCache.GetOrAdd(Tuple.Create(builder), tuple => builder());
			}

			public void Refresh()
			{
				keyValueCache.Remove(Tuple.Create(builder));
			}

			#endregion
		}

		private class MemoizedFunc<TArg, TVal>: IMemoizedFunc<TArg, TVal>
		{
			private readonly Func<TArg, TVal> builder;
			private readonly IKeyValueCache keyValueCache;

			public MemoizedFunc(IKeyValueCache keyValueCache, Func<TArg, TVal> builder)
			{
				this.keyValueCache = keyValueCache;
				this.builder = builder;
			}

			#region Implementation of IMemoizedFunc<T>

			public TVal Get(TArg arg)
			{
				return keyValueCache.GetOrAdd(Tuple.Create(builder, arg), tuple => builder(tuple.Item2));
			}

			public void Refresh(TArg arg)
			{
				keyValueCache.Remove(Tuple.Create(builder, arg));
			}

			#endregion
		}

		#endregion
	}
}