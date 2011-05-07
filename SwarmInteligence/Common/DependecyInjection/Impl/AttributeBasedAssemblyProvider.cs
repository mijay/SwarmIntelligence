using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Collections;

namespace Common.DependecyInjection.Impl
{
    public class AttributeBasedAssemblyProvider: IAssemblyProvider
    {
        #region Implementation of IAssemblyProvider

        public IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetCustomAttributes(typeof(ScanAssemblyAttribute), true).IsNotEmpty());
        }

        #endregion
    }
}