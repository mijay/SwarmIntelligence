using StructureMap;

namespace Common.DependecyInjection.Impl
{
    public interface IContainerFactory
    {
        Container Create();
    }
}