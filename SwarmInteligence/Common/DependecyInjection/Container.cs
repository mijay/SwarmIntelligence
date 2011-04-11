namespace Common.DependecyInjection
{
    public static class Container
    {
        private static readonly ContainerFactory factory =
            new ContainerFactory(new AttributeBasedAssemblyProvider());

        public static IContainer Build()
        {
            return new ContainerWrap(factory.Create());
        }
    }
}