using System;
using System.Diagnostics.Contracts;
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
            Contract.Requires(type != null && registry != null);
            if(!type.IsConcrete())
                return;

            registry.For(type).Singleton().Use(type);

            foreach(Type t in type.GetBaseTypes().Concat(type.GetInterfaces()))
                if(t.IsGenericType && type.IsGenericType)
                    registry
                        .For(t.GetGenericTypeDefinition())
                        .Singleton()
                        .Add(type.GetGenericTypeDefinition());
                else
                    registry.For(t).Singleton().Add(type);
        }

        public void Patch(PluginGraph pluginGraph)
        {
            Contract.Requires(pluginGraph != null);
            PluginFamily[] initialPluginFamilies = pluginGraph.PluginFamilies.ToArray();
            foreach(PluginFamily pluginFamily in initialPluginFamilies) {
                Type pluginType = pluginFamily.PluginType;
                if(pluginType.IsClosedGenerictType()) {
                    Type openPluginType = pluginType.GetGenericTypeDefinition();
                    if(pluginGraph.ContainsFamily(openPluginType)) {
                        PluginFamily openPluginFamily = pluginGraph.FindFamily(openPluginType);
                        CopyOpenGenericFamiltyToClosed(openPluginFamily, pluginFamily);
                    }
                }
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