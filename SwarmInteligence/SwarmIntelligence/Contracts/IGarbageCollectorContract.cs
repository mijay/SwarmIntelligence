using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Infrastructure.GrabgeCollection;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IGarbageCollector<,,>))]
	public abstract class IGarbageCollectorContract<TCoordinate, TNodeData, TEdgeData> : IGarbageCollector<TCoordinate, TNodeData, TEdgeData>
	{
		#region Implementation of IGarbageCollector<TCoordinate,TNodeData,TEdgeData>

		public MapBase<TCoordinate, TNodeData, TEdgeData> MapBase
		{
			get { throw new UreachableCodeException(); }
		}

		public void AttachTo(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase)
		{
			Contract.Requires(mapBase != null);
			Contract.Requires(MapBase == null);
			Contract.Ensures(MapBase == mapBase);
			throw new UreachableCodeException();
		}

		public void Collect()
		{
			Contract.Requires(MapBase != null);
			throw new UreachableCodeException();
		}

		#endregion
	}
}