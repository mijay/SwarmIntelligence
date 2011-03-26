using System;
using System.Diagnostics.Contracts;
using Common.Cache;

namespace Utils.DependencyInjection
{
    public class InstanceProvider: IInstanceProvider
    {
        private readonly IKeyValueCache cache;

        public InstanceProvider(IKeyValueCache cache)
        {
            Contract.Requires(cache != null);
            this.cache = cache;
        }

        public object GetInstance(Type type, InstanciatingBehaviour behaviour)
        {
            Contract.Requires(type != null);
            if(behaviour == InstanciatingBehaviour.AlwaysInstanciate)
                return CreateInstance(type);
            if(behaviour == InstanciatingBehaviour.ReuseInstance)
                return cache.GetOrSet(type, CreateInstance);
            throw new ArgumentException();
        }

        private static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}