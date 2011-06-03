using System;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    public abstract class GenericArgumentsExtractor
    {
        //todo: documentation+contracts
        public abstract void Extract(Type from, GenericArgumentsMap to);

        #region Nested type: CannotExtractException

        public class CannotExtractException: Exception {}

        #endregion

        public static GenericArgumentsExtractor Build(Type extractFrom)
        {
            throw new NotImplementedException();
        }
    }
}