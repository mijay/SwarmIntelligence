using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    public class GenericArgumentsMap
    {
        public GenericArgumentsMap(Type[] genericParametersForMappingTo)
        {
            Contract.Requires(!genericParametersForMappingTo.IsNullOrEmpty());
            Contract.Requires(Contract.ForAll(genericParametersForMappingTo, x => x.IsGenericParameter));

            typeArgumentsOrder = new SortedList<string, int>();
            genericParametersForMappingTo.ForEach((x, i) => typeArgumentsOrder.Add(x.Name, i));
        }

        private readonly IDictionary<string, Type> namedTypeArguments = new SortedList<string, Type>();
        private readonly IDictionary<string, int> typeArgumentsOrder;

        public Type this[string name]
        {
            get
            {
                Contract.Requires(!string.IsNullOrEmpty(name));
                if(!typeArgumentsOrder.ContainsKey(name))
                    throw new ArgumentException("Incorrect type argument name");
                return namedTypeArguments[name];
            }
            set
            {
                Contract.Requires(!string.IsNullOrEmpty(name) && value != null);
                Contract.Requires(!IsArgumentInitialized(name));
                if(!typeArgumentsOrder.ContainsKey(name))
                    throw new ArgumentException("Incorrect type argument name");
                namedTypeArguments[name] = value;
            }
        }

        [Pure]
        public bool IsArgumentInitialized(string name)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
            if(!typeArgumentsOrder.ContainsKey(name))
                throw new ArgumentException("Incorrect type argument name");
            return namedTypeArguments.ContainsKey(name);
        }

        [Pure]
        public Type[] ToArray()
        {
            return typeArgumentsOrder
                .OrderBy(x => x.Value)
                .Select(x => namedTypeArguments[x.Key])
                .ToArray();
        }
    }
}