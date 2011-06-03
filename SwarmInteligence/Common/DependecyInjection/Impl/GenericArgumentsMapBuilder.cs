using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;
using Common;

namespace Common.DependecyInjection.Impl
{
    public class GenericArgumentsMapBuilder
    {
        private readonly IList<int?> argumentsResolvers = new List<int?>();
        private readonly Type typeToExtractFrom;

        private GenericArgumentsMapBuilder(Type typeToExtractFrom)
        {
            this.typeToExtractFrom = typeToExtractFrom.GetGenericTypeDefinition();
        }

        public Type[] ExtractContext(Type concreteType)
        {
            if(concreteType.GetGenericTypeDefinition() != typeToExtractFrom)
                throw new ArgumentException();

            Type[] realArgs = concreteType.GetGenericArguments();
            return argumentsResolvers.Select(sourceArgIndex => realArgs[sourceArgIndex.Value]).ToArray();
        }

        private void AddResolver(int extractedTypeArgumentIndex, int sourceTypeArgumentIndex)
        {
            Contract.Requires(extractedTypeArgumentIndex >= 0 && sourceTypeArgumentIndex >= 0);
            argumentsResolvers.SetAt(extractedTypeArgumentIndex, sourceTypeArgumentIndex);
        }

        private bool Validate(int extractedTypeArgumentsCount)
        {
            return argumentsResolvers.Count == extractedTypeArgumentsCount
                   && argumentsResolvers.All(x => x != null);
        }

        public static GenericArgumentsMapBuilder BuildFor(Type t, GenericContext context)
        {
            var result = new GenericArgumentsMapBuilder(t);
            Type[] args = t.GetGenericArguments();
            for(int i = 0; i < args.Length; ++i)
                if(args[i].IsGenericParameter)
                    result.AddResolver(i, context.IndexOf(args[i]));
            return result.Validate(context.Count) ? result : null;
        }

        #region Nested type: GenericContext

        public class GenericContext: ReadOnlyList<Type>
        {
            public GenericContext(IEnumerable<Type> genericArguments): base(genericArguments.ToArray())
            {
                Contract.Requires(Contract.ForAll(genericArguments, x => x.IsGenericParameter));
            }
        }

        #endregion
    }
}