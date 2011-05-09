using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;
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

            if (type.IsOpenGeneric())
                ProcessOpenType(type, registry);
            else
                ProcessCloseType(type, registry);
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

        private static void ProcessCloseType(Type type, Registry registry)
        {
            type.GetBaseTypesAndInterfaces().ForEach(t => registry.For(t).Singleton().Add(type));
        }

        private static void ProcessOpenType(Type type, Registry registry)
        {
            foreach(Type t in type.GetBaseTypesAndInterfaces()) {
                if(!t.IsOpenGeneric())
                    // if `t` is not generic then during request
                    // we won't have enough data to create concrete closed `type`
                    // so ignore such types
                    continue;
                // todo: here we should understand partially opened types.
                registry
                        .For(t.GetGenericTypeDefinition())
                        .Singleton()
                        .Add(type.GetGenericTypeDefinition());
            }
        }

        private static void CopyOpenGenericFamiltyToClosed(PluginFamily openGenericFamily, PluginFamily closedGenericFamily)
        {
            PluginFamily mergedFamily =
                openGenericFamily.CreateTemplatedClone(closedGenericFamily.PluginType.GetGenericArguments());
            mergedFamily.ImportFrom(closedGenericFamily);
        }
    }
}