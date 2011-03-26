using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Collections;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;
using System.Linq;

namespace Test.ContextIndependendAnt
{
    public class Huj0: ICloneable
    {
        #region Implementation of ICloneable

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class Huj1: IEnumerable<int>
    {
        #region Implementation of IEnumerable

        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public class Huj2<T>: IEnumerable<T>
    {
        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    //public class Huj3<T>: System.Collections.Generic.IDictionary<int, T>
    //{
    //    #region Implementation of IEnumerable

    //    public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    #endregion

    //    #region Implementation of ICollection<KeyValuePair<int,T>>

    //    public void Add(KeyValuePair<int, T> item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Clear()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Contains(KeyValuePair<int, T> item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void CopyTo(KeyValuePair<int, T>[] array, int arrayIndex)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Remove(KeyValuePair<int, T> item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Count
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public bool IsReadOnly
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    #endregion

    //    #region Implementation of IDictionary<int,T>

    //    public bool ContainsKey(int key)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Add(int key, T value)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Remove(int key)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool TryGetValue(int key, out T value)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public T this[int key]
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public ICollection<int> Keys
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public ICollection<T> Values
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    #endregion
    //}

    public class Huj4<T>: IEnumerable<ICloneable<T>>
    {
        #region Implementation of IEnumerable

        public IEnumerator<ICloneable<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public class AllInterfacesBinding: IBindingGenerator
    {
        #region Implementation of IBindingGenerator

        public void Process(Type type, Func<IContext, object> scopeCallback, IKernel kernel)
        {
            if(type.IsInterface || type.IsAbstract)
                return;

            kernel.Bind(type).To(type).InScope(scopeCallback);

            type
                .GetInterfaces()
                .ForEach(i => {
                             if(!string.IsNullOrEmpty(i.FullName))
                                 kernel.Bind(i).To(type).InScope(scopeCallback);
                             else {
                                 var tuple = GetHuj(i, type);
                                 kernel.Bind(tuple.Item1).To(type).When(tuple.Item2).InScope(scopeCallback);
                             }
                         });
        }
        #endregion

        private static Tuple<Type, Func<IRequest, bool>> GetHuj(Type implementedInterfaceType, Type implementationType)
        {
            if(!implementedInterfaceType.IsGenericType)
                throw new InvalidOperationException("implementedInterfaceType has empty FullName but it is not a generic");

            var genericInterfaceType = implementedInterfaceType.GetGenericTypeDefinition();
            var interfaceImplementationGenericArguments = implementedInterfaceType.GetGenericArguments()
                .Select((a, i) => new GenericArgumentsTreeMatcher.GenericArgumentDescription { index = i, argument = a })
                .ToArray();
            var interfacePartialyAppliedArguments = interfaceImplementationGenericArguments
                .GroupJoin(implementationType.GetGenericArguments(),
                           x => x.argument,
                           x => x,
                           (intGArg, implGArgs) => implGArgs.IsEmpty()
                                                       ? intGArg
                                                       : new GenericArgumentsTreeMatcher.GenericArgumentDescription())
                .Where(x => x.argument != null)
                .ToArray();

            if (interfacePartialyAppliedArguments.IsEmpty())
                return new Tuple<Type, Func<IRequest, bool>>(genericInterfaceType, _ => true);

            var argumentsTreeMatcher = new GenericArgumentsTreeMatcher(interfacePartialyAppliedArguments);
            return new Tuple<Type, Func<IRequest, bool>>(genericInterfaceType, r => argumentsTreeMatcher.Match(r.Service));
        }

        private class GenericArgumentsTreeMatcher
        {
            private readonly IEnumerable<GenericArgumentDescription> argumentDescriptions;

            public GenericArgumentsTreeMatcher(IEnumerable<GenericArgumentDescription> argumentDescriptions)
            {
                this.argumentDescriptions = argumentDescriptions;
            }

            public bool Match(Type type)
            {
                return false;
            }

            public struct GenericArgumentDescription
            {
                public int index;
                public Type argument;
            }
        }
    }
}