using System;
using System.Diagnostics.Contracts;
using Common.DependecyInjection.Impl.GenericArgumentExtraction;

namespace Common.DependecyInjection.Impl
{
    public static class GenericArgumentsExtractorBuilder
    {
        //todo: return Instance ?
        public static Func<Type, Type[]> Build(Type @for, Type from)
        {
            Contract.Requires(@for != null && from != null && @for.IsGenericTypeDefinition);
            var context = new ExtractionContext(@for);
            TypeExtractor typeExtractor = TypeExtractor.Build(from, context);
            if(!context.IsResolved())
                return null;
            return source => {
                       GenericArgumentsMap map = context.GetInitialMap();
                       try {
                           typeExtractor.Extract(source, map);
                       }
                       catch(TypeExtractor.CannotExtractException) {
                           return null;
                       }
                       return map.ToArray();
                   };
        }
    }
}