using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Memoization;

namespace Common.DependencyInjection
{
    public class TypeResolver: ITypeResolver
    {
        private readonly Func<Type, TypeResolvingOptions, IEnumerable<Type>> cachedSelect;
        private readonly ITypeProvider typeProvider;

        public TypeResolver(ITypeProvider typeProvider, IMemoizer memoizer)
        {
            Contract.Requires(typeProvider != null && memoizer != null);
            this.typeProvider = typeProvider;
            typeProvider.BuildUp();
            cachedSelect = memoizer.Memoize<Type, TypeResolvingOptions, IEnumerable<Type>>(SelectInternal);
        }

        public IEnumerable<Type> Select(Type type, TypeResolvingOptions options)
        {
            return cachedSelect(type, options);
        }

        private IEnumerable<Type> SelectInternal(Type type, TypeResolvingOptions options)
        {
            Contract.Requires(type != null);
            IEnumerable<Type> result = typeProvider.Types.Where(type.IsAssignableFrom);
            result = FilterByOptions(options, result);
            return result.ToArray();
        }

        private static IEnumerable<Type> FilterByOptions(TypeResolvingOptions options, IEnumerable<Type> result)
        {
            if(options.HasFlag(TypeResolvingOptions.ExcludeInterfaces))
                result = result.Where(x => !x.IsInterface);
            if(options.HasFlag(TypeResolvingOptions.ExcludeAbstractClasses))
                result = result.Where(x => !x.IsAbstract);
            if(options.HasFlag(TypeResolvingOptions.ExcludeTypesWithGenericArguments))
                result = result.Where(x => !x.ContainsGenericParameters);
            if(options.HasFlag(TypeResolvingOptions.ExludeTypesWithoutAccessibleConstructor))
                result = result.Where(x => x.GetConstructors().Any());
            return result;
        }
    }
}