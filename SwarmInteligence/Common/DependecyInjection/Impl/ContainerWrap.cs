using System.Collections.Generic;
using System.Linq;
using Common.Collections;
using StructureMap;

namespace Common.DependecyInjection.Impl
{
    public class ContainerWrap: IContainer
    {
        private readonly Container container;

        public ContainerWrap(Container container)
        {
            this.container = container;
        }

        #region Implementation of IContainer

        public T Get<T>()
        {
            try {
                return container.GetInstance<T>();
            }
            catch(StructureMapException e) {
                throw new ContainerException(e.Message, e);
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            T[] result;
            try {
                result = container.GetAllInstances<T>().ToArray();
            }
            catch(StructureMapException e) {
                throw new ContainerException(e.Message, e);
            }
            if(result.IsEmpty())
                throw new ContainerException("Nothing was found");
            return result;
        }

        #endregion
    }
}