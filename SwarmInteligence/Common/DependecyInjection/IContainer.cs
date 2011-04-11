using System.Collections.Generic;

namespace Common.DependecyInjection
{
    public interface IContainer
    {
        T Get<T>();
        IEnumerable<T> GetAll<T>();
    }
}