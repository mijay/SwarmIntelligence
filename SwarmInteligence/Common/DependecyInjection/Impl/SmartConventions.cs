using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;
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

            if(type.IsGenericType)
                ProcessGenericType(type, registry);
            else
                ProcessCloseType(type, registry);
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

        private static void ProcessCloseType(Type type, Registry registry)
        {
            type.GetBaseTypesAndInterfaces().ForEach(t => registry.For(t).Singleton().Add(type));
        }

        private static void ProcessGenericType(Type type, Registry registry)
        {
            foreach(Type t in type.GetBaseTypesAndInterfaces()) {
                if(!t.IsOpenGeneric())
                    // if `t` is not generic then during request
                    // we won't have enough data to create concrete closed `type`
                    // so ignore such types
                    continue;
                // todo: here we should understand partially opened types.
                // todo: creates smth like a GenericArgumentsMap from `t`-s generic params to `type`-s
                // todo: then if such map is trivial (one-to-one) then use following code
                // todo: if map will not be surjective then above code (ignore and continue)
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