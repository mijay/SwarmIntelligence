using System.Diagnostics.Contracts;
using Common.Collections;
using StructureMap.Graph;

namespace Common.DependecyInjection.Impl
{
    public class ContainerFactory: IContainerFactory
    {
        private readonly IAssemblyProvider assemblyProvider;

        public ContainerFactory(IAssemblyProvider assemblyProvider)
        {
            Contract.Requires(assemblyProvider != null);
            this.assemblyProvider = assemblyProvider;
        }

        public StructureMap.Container Create()
        {
            return new StructureMap.Container(x => x.Scan(SetupScanner));
        }

        private void SetupScanner(IAssemblyScanner assemblyScanner)
        {
            assemblyProvider.GetAssemblies().ForEach(assemblyScanner.Assembly);
            assemblyScanner.Convention<SmartConventions>();
        }
    }
}