using System.Collections.Generic;
using System.Reflection;

namespace Common.DependecyInjection
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}