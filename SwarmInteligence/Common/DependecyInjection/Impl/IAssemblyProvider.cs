using System.Collections.Generic;
using System.Reflection;

namespace Common.DependecyInjection.Impl
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}