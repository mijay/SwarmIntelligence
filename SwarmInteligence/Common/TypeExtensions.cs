using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
	public static class TypeExtensions
	{
		public static IEnumerable<Type> GetBaseTypesAndInterfaces(this Type type)
		{
			Requires.NotNull(type);
			return type.GetBaseTypes().Concat(type.GetInterfaces());
		}

		public static IEnumerable<Type> GetBaseTypes(this Type type)
		{
			Requires.NotNull(type);
			Type current = type.BaseType;

			while(current != typeof(object) && current != typeof(ValueType)) {
				yield return current;
				current = current.BaseType;
			}
		}

		public static bool IsClosedGenerictType(this Type type)
		{
			Requires.NotNull(type);
			return type.IsGenericType && !type.ContainsGenericParameters;
		}

		public static bool IsOpenGenerictType(this Type type)
		{
			Requires.NotNull(type);
			return type.IsGenericType && type.ContainsGenericParameters;
		}
	}
}