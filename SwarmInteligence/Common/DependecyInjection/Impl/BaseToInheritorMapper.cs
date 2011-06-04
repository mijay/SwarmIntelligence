using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            return new MappedInheritorInstance((extractor as OpenTypeExtractor), context.Arguments);
        }

        #region Nested type: MappedInheritorInstance

        public class MappedInheritorInstance: Instance
        {
            private readonly IList<Type> genericParams;
            private readonly Extractor[] argumentExtractors;

            public MappedInheritorInstance(OpenTypeExtractor openTypeExtractor, IList<Type> genericParams)
            {
                Contract.Requires(openTypeExtractor != null && genericParams != null);
                this.genericParams = genericParams;
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
                throw new NotImplementedException();
                return base.CloseType(types);
            }

            #endregion
        }

        #endregion
    }
}