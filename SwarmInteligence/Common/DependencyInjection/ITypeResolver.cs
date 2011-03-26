using System;
using System.Collections.Generic;

namespace Common.DependencyInjection
{
    public interface ITypeResolver {
        IEnumerable<Type> Select(Type type, TypeResolvingOptions options);
    }
}