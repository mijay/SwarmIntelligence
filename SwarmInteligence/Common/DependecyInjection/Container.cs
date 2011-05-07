using System;
using System.Threading;
using Common.DependecyInjection.Impl;

namespace Common.DependecyInjection
{
    public static class ContainerHost
    {
        private static readonly Lazy<IContainer> container =
            new Lazy<IContainer>(() => new ContainerWrap(new ContainerFactory(new AttributeBasedAssemblyProvider()).Create()),
                                 LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return container.Value; }
        }
    }
}