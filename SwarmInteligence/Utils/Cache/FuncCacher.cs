using System;

namespace Utils.Cache
{
    public class FuncCacher: IFuncCacher
    {
        private readonly IKeyValueCache cache;

        public FuncCacher(IKeyValueCache cache)
        {
            this.cache = cache;
        }

        #region Implementation of IFuncCacher

        public Func<TVal> MakeCached<TVal>(Func<TVal> func)
        {
            return () => cache.GetOrSet(Tuple.Create(func), tuple => tuple.Item1());
        }

        public Func<TArg, TVal> MakeCached<TArg, TVal>(Func<TArg, TVal> func)
        {
            return arg => cache.GetOrSet(Tuple.Create(func, arg), tuple => tuple.Item1(tuple.Item2));
        }

        public Func<TArg1, TArg2, TVal> MakeCached<TArg1, TArg2, TVal>(Func<TArg1, TArg2, TVal> func)
        {
            return (arg1, arg2) => cache.GetOrSet(Tuple.Create(func, arg1, arg2), tuple => tuple.Item1(tuple.Item2, tuple.Item3));
        }

        #endregion
    }
}