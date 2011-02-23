using System;

namespace Utils.Cache
{
    public interface IFuncCacher
    {
        Func<TVal> MakeCached<TVal>(Func<TVal> func);
        Func<TArg, TVal> MakeCached<TArg, TVal>(Func<TArg, TVal> func);
        Func<TArg1, TArg2, TVal> MakeCached<TArg1, TArg2, TVal>(Func<TArg1, TArg2, TVal> func);
    }
}