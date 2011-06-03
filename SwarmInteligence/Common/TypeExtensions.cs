using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

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

        public static bool IsOpenGenerictType(this Type type)
        {
            Contract.Requires(type != null);
            return type.IsGenericType && type.ContainsGenericParameters;
        }

        public static IEnumerable<Type> GetBaseTypesAndInterfaces(this Type type)
        {
            Contract.Requires(type != null);
            return type.GetBaseTypes().Concat(type.GetInterfaces());
        }
    }
}