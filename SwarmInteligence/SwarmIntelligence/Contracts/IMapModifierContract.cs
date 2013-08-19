using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core;
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
			throw new UnreachableCodeException();
		}

		#endregion

		#region Implementation of IMapModifier<TCoordinate,TNodeData,TEdgeData>

		public World<TCoordinate, TNodeData, TEdgeData> World
		{
			get
			{
				Contract.Ensures(Contract.Result<World<TCoordinate, TNodeData, TEdgeData>>() != null);
				throw new UnreachableCodeException();
			}
		}

		public void AddAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
		{
			Contract.Requires(ant != null);
			Contract.Requires(World.Topology.Lays(coordinate));
			throw new UnreachableCodeException();
		}

		public void RemoveAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
		{
			Contract.Requires(ant != null);
			Contract.Requires(World.Topology.Lays(coordinate));
			//Contract.Requires(Map.ForcedGet(coordinate).Contains(ant));
			throw new UnreachableCodeException();
		}

		#endregion
	}
}