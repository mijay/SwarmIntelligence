using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common.Collections;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    public class ExtractionContext
    {
        private static readonly Comparer<Type> typeParameterComparer = new Comparer<Type>((x, y) => x.Name.CompareTo(y.Name));
        private readonly ISet<Type> resolved = new SortedSet<Type>(typeParameterComparer);

        public ExtractionContext(Type @for)
        {
            Contract.Requires(@for != null);
            Arguments = new ReadOnlyList<Type>(@for.GetGenericArguments());
        }

        public IList<Type> Arguments { get; private set; }

        public void MarkResolvable(Type genericArgument)
        {
            Contract.Requires(genericArgument != null && Arguments.Contains(genericArgument));
            resolved.Add(genericArgument);
        }

        [Pure]
        public bool IsResolved()
        {
            return resolved.SetEquals(Arguments);
        }

        [Pure]
        public GenericArgumentsMap GetInitialMap()
        {
            Contract.Requires(IsResolved());

            return new GenericArgumentsMap(Arguments);
        }
    }
}