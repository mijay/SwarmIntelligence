using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Common.Collections;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    public class OpenTypeExtractor: Extractor
    {
        public OpenTypeExtractor(Type openType, ExtractionContext typeExtractionContext)
        {
            Contract.Requires(openType != null && openType.IsOpenGenerictType());
            TypeGenericDefinition = openType.GetGenericTypeDefinition();
            NestedExtractors = openType.GetGenericArguments()
                .Select(x => Build(x, typeExtractionContext))
                .ToArray();
        }

        #region Overrides of Extractor

        public override void Extract(Type from, GenericArgumentsMap to)
        {
            if(!from.IsClosedGenerictType() || from.GetGenericTypeDefinition() != TypeGenericDefinition)
                throw new CannotExtractException(
                    string.Format("OpenTypeExtractor expected to get type like {0} but received {1}.",
                                  TypeGenericDefinition.FullName, from.FullName));

            try {
                from.GetGenericArguments()
                    .Zip(NestedExtractors,
                         (type, extractor) => new { type, extractor })
                    .ForEach(x => x.extractor.Extract(x.type, to));
            }
            catch(CannotExtractException e) {
                throw new CannotExtractException(string.Format("Cannot extract type {0}.", from.FullName), e);
            }
        }

        #endregion

        public Extractor[] NestedExtractors { get; private set; }

        public Type TypeGenericDefinition { get; private set; }
    }
}