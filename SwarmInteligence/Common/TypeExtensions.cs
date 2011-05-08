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

            while(current != typeof(object) && current != typeof(ValueType)) {
                yield return current;
                current = current.BaseType;
            }
        }

        public static bool IsClosedGenerictType(this Type type)
        {
            Contract.Requires(type != null);
            return type.IsGenericType && !type.ContainsGenericParameters;
        }
    }
}