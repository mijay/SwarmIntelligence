using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common.DependecyInjection.Impl.GenericArgumentExtraction
{
    public class TypeParameterExtractor: GenericArgumentsExtractor
    {
        public Type GenericParameter { get; internal set; }

        public TypeParameterExtractor(Type genericParameter)
        {
            Contract.Requires(genericParameter != null && genericParameter.IsGenericParameter);
            GenericParameter = genericParameter;
        }

        #region Overrides of GenericArgumentsExtractor

        public override void Extract(Type from, GenericArgumentsMap to)
        {
            //todo: check constraints?
            to[GenericParameter.Name] = from;
        }

        #endregion
    }
}