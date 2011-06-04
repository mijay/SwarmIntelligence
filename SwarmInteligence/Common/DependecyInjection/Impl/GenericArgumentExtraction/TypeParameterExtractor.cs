using System;
using System.Diagnostics.Contracts;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    public class TypeParameterExtractor: Extractor
    {
        public TypeParameterExtractor(Type genericParameter, ExtractionContext typeExtractionContext)
        {
            Contract.Requires(genericParameter != null && genericParameter.IsGenericParameter);
            GenericParameterName = genericParameter.Name;
            typeExtractionContext.MarkResolvable(genericParameter);
        }

        #region Overrides of Extractor

        public override void Extract(Type from, GenericArgumentsMap to)
        {
            if(!to.HasValueFor(GenericParameterName))
                //todo: check constraints?
                to[GenericParameterName] = from;
            else if(to[GenericParameterName] != from)
                throw new CannotExtractException(
                    string.Format("Parameter {0} has already been set to {1} and cannot be changed to {2}.",
                        GenericParameterName, to[GenericParameterName].FullName, from.Name));
        }

        #endregion

        public string GenericParameterName { get; internal set; }
    }
}