using System;
using System.Diagnostics.Contracts;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    [ContractClass(typeof(TypeExtractorContract))]
    public abstract class TypeExtractor
    {
        public abstract void Extract(Type from, GenericArgumentsMap to);

        public static TypeExtractor Build(Type extractFrom, ExtractionContext extractionContext)
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

    [ContractClassFor(typeof(TypeExtractor))]
    internal abstract class TypeExtractorContract: TypeExtractor
    {
        public override void Extract(Type from, GenericArgumentsMap to)
        {
            Contract.Requires(from != null && to != null);
        }
    }
}