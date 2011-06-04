using System;
using System.Diagnostics.Contracts;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    [ContractClass(typeof(ExtractorContract))]
    public abstract class Extractor
    {
        public abstract void Extract(Type from, GenericArgumentsMap to);

        public static Extractor Build(Type extractFrom, ExtractionContext extractionContext)
        {
            if(extractFrom.IsGenericParameter)
                return new TypeParameterExtractor(extractFrom, extractionContext);
            if(extractFrom.IsOpenGenerictType())
                return new OpenTypeExtractor(extractFrom, extractionContext);
            return new ClosedTypeExtractor(extractFrom);
        }

        #region Nested type: CannotExtractException

        public class CannotExtractException: Exception
        {
            public CannotExtractException(string message): base(message) {}
            public CannotExtractException(string message, CannotExtractException innerException): base(message, innerException) {}
        }

        #endregion
    }

    [ContractClassFor(typeof(Extractor))]
    internal abstract class ExtractorContract: Extractor
    {
        public override void Extract(Type from, GenericArgumentsMap to)
        {
            Contract.Requires(from != null && to != null);
        }
    }
}