using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    public class OpenTypeExtractor: GenericArgumentsExtractor
    {
        private GenericArgumentsExtractor[] nestedExtractors;
        public Type TypeGenericDefinition { get; private set; }

        public OpenTypeExtractor(Type openType)
        {
            Contract.Requires(openType != null && openType.IsOpenGenerictType());
            TypeGenericDefinition = openType.GetGenericTypeDefinition();
            nestedExtractors = openType.GetGenericArguments()
                .Select(GenericArgumentsExtractor.Build)
                .ToArray();
        }

        #region Overrides of GenericArgumentsExtractor

        public override void Extract(Type from, GenericArgumentsMap to)
        {
            if (!from.IsClosedGenerictType() || from.GetGenericTypeDefinition() != TypeGenericDefinition)
                throw new CannotExtractException();

            from.GetGenericArguments()
                .Zip(nestedExtractors,
                     (type, extractor) => new { type, extractor })
                .ForEach(x => x.extractor.Extract(x.type, to));
        }

        #endregion
    }
}