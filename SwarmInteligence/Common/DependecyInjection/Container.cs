using System;
using System.Threading;
using Common.DependecyInjection.Impl;

namespace Common.DependecyInjection
{
    public static class ContainerHost
    {
        private static readonly Lazy<IContainer> container;
        private static IContainerFactory factory;
        private static bool built;

        static ContainerHost()
        {
            factory = new ContainerFactory(new AttributeBasedAssemblyProvider(), new SmartConventions());
            container = new Lazy<IContainer>(() => {
                                                 built = true;
                                                 return new ContainerWrap(Factory.Create());
                                             },
                                             LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public static IContainer Container
        {
            get { return container.Value; }
        }

        public static IContainerFactory Factory
        {
            get { return factory; }
            set
            {
                if(built)
                    throw new InvalidOperationException();
                factory = value;
            }
        }
    }
}