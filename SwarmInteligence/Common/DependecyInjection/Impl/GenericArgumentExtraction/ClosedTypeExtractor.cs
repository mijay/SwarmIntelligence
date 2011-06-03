using System;
using System.Diagnostics.Contracts;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    public class ClosedTypeExtractor: GenericArgumentsExtractor
    {
        public Type Type { get; private set; }

        public ClosedTypeExtractor(Type closedType)
        {
            Contract.Requires(closedType != null && !closedType.IsOpenGenerictType());
            Type = closedType;
        }

        #region Overrides of GenericArgumentsExtractor

        public override void Extract(Type from, GenericArgumentsMap to)
        {
            if (from != Type)
                throw new CannotExtractException();
        }

        #endregion
    }
}