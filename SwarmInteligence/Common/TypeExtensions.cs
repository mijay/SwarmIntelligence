using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            Contract.Requires(type != null);
            Type current = type.BaseType;

            while (current != null)
            {
                yield return current;
                current = current.BaseType;
            }
        }
    }
}