using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Common.Cache;
using Common.Memoization;

namespace Utils.DependencyInjection
{
    public static class ContainerHost
    {
        private const TypeResolvingOptions typeResolvingOptions = TypeResolvingOptions.ExcludeAbstractClasses
                                                                  | TypeResolvingOptions.ExcludeInterfaces
                                                                  | TypeResolvingOptions.ExcludeTypesWithGenericArguments
                                                                  | TypeResolvingOptions.ExludeTypesWithoutAccessibleConstructor;

        private static readonly InstanceProvider instanceProvider;
        private static readonly TypeResolver typeResolver;

        static ContainerHost()
        {
            instanceProvider = new InstanceProvider(new ConcurentDictionaryCache(new ConcurrentDictionary<object, object>()));
            typeResolver = new TypeResolver(new TypeProvider(),
                                            new Memoizer(new ConcurentDictionaryCache(new ConcurrentDictionary<object, object>())));
        }

        public static T Get<T>()
        {
            return GetAll<T>().Single();
        }

        public static IEnumerable<T> GetAll<T>()
        {
            return typeResolver
                .Select(typeof(T), typeResolvingOptions)
                .Select(x => instanceProvider.GetInstance(x, InstanciatingBehaviour.ReuseInstance))
                .Cast<T>()
                .ToArray();
        }
    }
}