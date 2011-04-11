using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace Common.DependecyInjection
{
    public class ContainerWrap: IContainer
    {
        private readonly StructureMap.Container container;

        public ContainerWrap(StructureMap.Container container)
        {
            this.container = container;
        }

        #region Implementation of IContainer

        public T Get<T>()
        {
            return container.GetInstance<T>();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return container.GetAllInstances<T>().ToArray();
        }

        #endregion
    }
}