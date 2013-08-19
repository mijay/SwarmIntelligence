using System;
using Common;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Specialized;

namespace SILibrary.BuildUp
{
	internal class Configured<TCoordinate, TNodeData, TEdgeData>: DisposableBase,
	                                                              SystemBuilder.IConfigured<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly IMapModifier<TCoordinate, TNodeData, TEdgeData> mapModifier;
		private readonly World<TCoordinate, TNodeData, TEdgeData> world;

		public Configured(BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld)
		{
			world = buildingWorld.Build();
			mapModifier = world.GetModifier();
		}

		protected override void DisposeManaged()
		{
			base.DisposeManaged();
			mapModifier.Dispose();
		}

		#region Implementation of IConfigured<TCoordinate,TNodeData,TEdgeData>

		public SystemBuilder.IConfigured<TCoordinate, TNodeData, TEdgeData> SeedData(Action<IEdgesDataLayer<TCoordinate, TEdgeData>> seed)
		{
			CheckDisposedState();
			seed(world.EdgesData);
			return this;
		}

		public SystemBuilder.IConfigured<TCoordinate, TNodeData, TEdgeData> SeedData(Action<INodesDataLayer<TCoordinate, TNodeData>> seed)
		{
			CheckDisposedState();
			seed(world.NodesData);
			return this;
		}

		public SystemBuilder.IConfigured<TCoordinate, TNodeData, TEdgeData> SeedAnts(Action<IMapModifier<TCoordinate, TNodeData, TEdgeData>> seed)
		{
			CheckDisposedState();
			seed(mapModifier);
			return this;
		}

		public World<TCoordinate, TNodeData, TEdgeData> Build()
		{
			CheckDisposedState();
			Dispose();
			return world;
		}

		#endregion
	}
}