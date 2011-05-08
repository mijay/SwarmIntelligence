using System.Diagnostics.Contracts;
using Common.Collections;
using StructureMap;
using StructureMap.Graph;

namespace Common.DependecyInjection.Impl
{
    public class ContainerFactory: IContainerFactory
    {
        private readonly IAssemblyProvider assemblyProvider;
        private readonly IConventions conventions;

        public ContainerFactory(IAssemblyProvider assemblyProvider, IConventions conventions)
        {
            Contract.Requires(assemblyProvider != null);
            this.assemblyProvider = assemblyProvider;
            this.conventions = conventions;
        }

        #region IContainerFactory Members

        public Container Create()
        {
            return new Container(x => x.Scan(SetupScanner));
        }

        #endregion

        private void SetupScanner(IAssemblyScanner assemblyScanner)
        {
            assemblyProvider.GetAssemblies().ForEach(assemblyScanner.Assembly);
            assemblyScanner.With(conventions);
            assemblyScanner.ModifyGraphAfterScan(conventions.Patch);
        }
    }
}