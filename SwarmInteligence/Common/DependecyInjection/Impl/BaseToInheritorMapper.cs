using System;
using System.Diagnostics.Contracts;
using Common.Collections;
using Common.DependecyInjection.Impl.GenericArgumentExtraction;
using StructureMap;
using StructureMap.Pipeline;

namespace Common.DependecyInjection.Impl
{
    public static class BaseToInheritorMapper
    {
        public static Instance Build(Type @for, Type from)
        {
            Contract.Requires(@for != null && from != null);
            Contract.Requires(!@for.IsGenericType || @for.IsGenericTypeDefinition);

            var context = new ExtractionContext(@for);
            Extractor extractor = Extractor.Build(from, context);
            if(!context.IsResolved())
                return null;

            if(extractor is TypeParameterExtractor)
                throw new InvalidOperationException();
            if(extractor is ClosedTypeExtractor)
                return new ConfiguredInstance((extractor as ClosedTypeExtractor).Type);
            return new MappedInheritorInstance(@for, (extractor as OpenTypeExtractor));
        }

        #region Nested type: MappedInheritorInstance

        public class MappedInheritorInstance: Instance
        {
            private readonly Extractor[] argumentExtractors;
            private readonly Type[] genericParams;
            private readonly Type inheritorType;

            public MappedInheritorInstance(Type inheritorType, OpenTypeExtractor openTypeExtractor)
            {
                Contract.Requires(openTypeExtractor != null && inheritorType != null && inheritorType.IsGenericTypeDefinition);
                this.inheritorType = inheritorType;
                genericParams = inheritorType.GetGenericArguments();
                argumentExtractors = openTypeExtractor.NestedExtractors;
            }

            #region Overrides of Instance

            protected override string getDescription()
            {
                return "Instance which handles request for generic base services";
            }

            protected override object build(Type pluginType, BuildSession session)
            {
                throw new NotSupportedException(
                    "This Instance cannot create services by itself - just create child one using CloseType()");
            }

            public override Instance CloseType(Type[] types)
            {
                if(types.Length != genericParams.Length)
                    throw new InvalidOperationException();

                var argumentsMap = new GenericArgumentsMap(genericParams);
                try {
                    types
                        .LazyZip(argumentExtractors)
                        .ForEach(x => x.Value.Extract(x.Key, argumentsMap));
                }
                catch(Extractor.CannotExtractException) {
                    return null;
                }

                Type genericType = inheritorType.MakeGenericType(argumentsMap.ToArray());
                return new ConfiguredInstance(genericType);
            }

            #endregion
        }

        #endregion
    }
}