using System;
using System.Diagnostics.Contracts;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    public class ClosedTypeExtractor: TypeExtractor
    {
        public ClosedTypeExtractor(Type closedType)
        {
            Contract.Requires(closedType != null && !closedType.IsOpenGenerictType());
            Type = closedType;
        }

        #region Overrides of TypeExtractor

        public override void Extract(Type from, GenericArgumentsMap to)
        {
            if(from != Type)
                throw new CannotExtractException(
                    string.Format("Expected to get type equal to {0} but received {1}.",
                                  Type.FullName, from.FullName));
        }

        #endregion

        public Type Type { get; private set; }
    }
}