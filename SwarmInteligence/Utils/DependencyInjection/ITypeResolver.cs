using System;
using System.Collections.Generic;

namespace Utils.DependencyInjection
{
    public interface ITypeResolver {
        IEnumerable<Type> Select(Type type, TypeResolvingOptions options);
    }
}