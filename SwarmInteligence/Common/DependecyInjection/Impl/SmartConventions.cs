using System;
using System.Linq;
using Common.Collections;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Common.DependecyInjection.Impl
{
    public class SmartConventions: IRegistrationConvention
    {
        #region Implementation of IRegistrationConvention

        public void Process(Type type, Registry registry)
        {
            if(!type.IsConcrete())
                return;
            if(type.IsGenericType) {
                if (type.GetGenericArguments().Count() == 0)
                    ProcessOpenType(type, registry);
                else
                    ProcessOpenClosedType(type, registry);
            }
            else
                ProcessClosedType(type, registry);
        }

        #endregion

        private void ProcessOpenType(Type type, Registry registry)
        {
            //todo: Implement !!
            return;
        }

        private void ProcessOpenClosedType(Type type, Registry registry)
        {
            throw new NotImplementedException();
        }

        private void ProcessClosedType(Type type, Registry registry)
        {
            Type[] types = type.GetBaseTypes()
                .Where(x => !x.IsConcrete())
                .Concat(type.GetInterfaces())
                .Concat(type)
                .ToArray();
            foreach(Type t in types)
                registry.For(t)
                    .LifecycleIs(InstanceScope.Singleton)
                    .Add(type);
        }
    }
}