using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.DependencyInjection
{
    public class TypeProvider: ITypeProvider
    {
        private IEnumerable<Type> types;

        #region ITypeProvider Members

        public IEnumerable<Type> Types
        {
            get
            {
                if(types == null)
                    throw new InvalidOperationException("BuildUp has not been called");
                return types;
            }
        }

        public void BuildUp()
        {
            if(types == null) {
                types = ScanAssemblies().ToArray();
                types = types.Concat(types.SelectMany(GetNestedTypesRecursive)).ToArray();
            }
        }

        #endregion

        private static IEnumerable<Type> ScanAssemblies()
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Distinct();
        }

        private static IEnumerable<Type> GetNestedTypesRecursive(Type t)
        {
            var nestedTypes = t.GetNestedTypes();

            return nestedTypes
                .SelectMany(GetNestedTypesRecursive)
                .Concat(nestedTypes);
        }
    }
}