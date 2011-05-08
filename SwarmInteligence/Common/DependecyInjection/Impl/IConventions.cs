using StructureMap.Graph;

namespace Common.DependecyInjection.Impl
{
    public interface IConventions: IRegistrationConvention
    {
        void Patch(PluginGraph pluginGraph);
    }
}