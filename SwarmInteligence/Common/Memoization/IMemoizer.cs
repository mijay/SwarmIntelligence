﻿using System;

namespace Common.Memoization
{
    public interface IMemoizer
    {
        Func<TVal> Memoize<TVal>(Func<TVal> func);
        Func<TArg, TVal> Memoize<TArg, TVal>(Func<TArg, TVal> func);
        Func<TArg1, TArg2, TVal> Memoize<TArg1, TArg2, TVal>(Func<TArg1, TArg2, TVal> func);
    }
}