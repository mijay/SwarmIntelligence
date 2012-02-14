using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Specialized;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IMapModifier<,,>))]
	public abstract class IMapModifierContract<TCoordinate, TNodeData, TEdgeData>: IMapModifier<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region Implementation of IDisposable

		public void Dispose()
		{
			throw new UreachableCodeException();
		}

		#endregion

		#region Implementation of IMapModifier<TCoordinate,TNodeData,TEdgeData>

		public IMap<TCoordinate, TNodeData, TEdgeData> Map
		{
			get
			{
				Contract.Ensures(Contract.Result<IMap<TCoordinate, TNodeData, TEdgeData>>() != null);
				throw new UreachableCodeException();
			}
		}

		public void AddAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
		{
			Contract.Requires(ant != null);
			Contract.Requires(Map.Topology.Lays(coordinate));
			throw new UreachableCodeException();
		}

		public void RemoveAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
		{
			Contract.Requires(ant != null);
			Contract.Requires(Map.Topology.Lays(coordinate));
			//Contract.Requires(Map.Get(coordinate).Contains(ant));
			throw new UreachableCodeException();
		}

		#endregion
	}
}