using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Common.DependecyInjection
{
    public class ContainerFactory
    {
        private readonly IAssemblyProvider assemblyProvider;

        public ContainerFactory(IAssemblyProvider assemblyProvider)
        {
            Contract.Requires(assemblyProvider != null);
            this.assemblyProvider = assemblyProvider;
        }

        public StructureMap.Container Create()
        {
            return new StructureMap.Container(x => x.Scan(SetupScanner));
        }

        private void SetupScanner(IAssemblyScanner assemblyScanner)
        {
            assemblyProvider.GetAssemblies().ForEach(assemblyScanner.Assembly);
            assemblyScanner.Convention<SmartConventions>();
        }

        #region Nested type: SmartConventions

        private class SmartConventions: IRegistrationConvention
        {
            #region Implementation of IRegistrationConvention

            public void Process(Type type, Registry registry)
            {
                if(!type.IsConcrete())
                    return;
                if(!type.IsGenericType)
                    ProcessClosedType(type, registry);
            }

            private void ProcessClosedType(Type type, Registry registry)
            {
                Type[] types = type.GetBaseTypeAndSelf()
                    .Concat(type.GetInterfaces()).ToArray();
                foreach(Type t in types)
                    //if(t.IsGenericType)
                    registry.For(t)
                        .LifecycleIs(InstanceScope.Singleton)
                        .Use(type);
            }

            #endregion
        }

        #endregion
    }
}