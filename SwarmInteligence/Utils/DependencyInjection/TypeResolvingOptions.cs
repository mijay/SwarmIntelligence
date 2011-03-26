using System;

namespace Utils.DependencyInjection
{
    [Flags]
    public enum TypeResolvingOptions
    {
        ExcludeInterfaces = 0,
        ExcludeAbstractClasses = 1,
        ExcludeTypesWithGenericArguments = 2,
        ExludeTypesWithoutAccessibleConstructor = 4
    }
}