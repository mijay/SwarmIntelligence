using System;
using System.Linq;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Common.DependecyInjection.Impl
{
    public class SmartConventions: IConventions
    {
        #region IConventions Members

        public void Process(Type type, Registry registry)
        {
            if(!type.IsConcrete())
                return;

            registry.For(type).Singleton().Use(type);

            foreach(Type t in type.GetBaseTypesAndInterfaces()) {
                var instance = BaseToInheritorMapper.Build(type, t);
                if (instance != null)
                    registry
                        .For(!t.IsGenericType ? t : t.GetGenericTypeDefinition())
                        .Singleton()
                        .Use(instance);
            }
        }

        public void Patch(PluginGraph pluginGraph)
        {
            foreach(PluginFamily pluginFamily in pluginGraph.PluginFamilies.ToArray())
                if(pluginFamily.PluginType.IsClosedGenerictType()) {
                    Type openPluginType = pluginFamily.PluginType.GetGenericTypeDefinition();
                    if(pluginGraph.ContainsFamily(openPluginType))
                        CopyOpenGenericFamiltyToClosed(pluginGraph.FindFamily(openPluginType), pluginFamily);
                }
        }

        #endregion

        private static void CopyOpenGenericFamiltyToClosed(PluginFamily openGenericFamily, PluginFamily closedGenericFamily)
        {
            PluginFamily mergedFamily =
                openGenericFamily.CreateTemplatedClone(closedGenericFamily.PluginType.GetGenericArguments());
            mergedFamily.ImportFrom(closedGenericFamily);
        }
    }
}