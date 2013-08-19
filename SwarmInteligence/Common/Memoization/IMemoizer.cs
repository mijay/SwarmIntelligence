using System;

namespace Common.Memoization
{
	public interface IMemoizer
	{
		IMemoizedFunc<TVal> Memoize<TVal>(Func<TVal> func);
		IMemoizedFunc<TArg, TVal> Memoize<TArg, TVal>(Func<TArg, TVal> func);
	}
}