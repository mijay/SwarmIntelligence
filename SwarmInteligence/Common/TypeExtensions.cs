using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetBaseTypeAndSelf(this Type type)
        {
            Contract.Requires(type != null);
            Type current = type;

            while(current != null) {
                yield return current;
                current = current.BaseType;
            }
        }
    }
}