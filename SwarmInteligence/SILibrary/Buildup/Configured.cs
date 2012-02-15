using System;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Specialized;

namespace SILibrary.Buildup
{
	internal class Configured<TCoordinate, TNodeData, TEdgeData>: IConfigured<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		private readonly IMapModifier<TCoordinate, TNodeData, TEdgeData> mapModifier;
		private readonly World<TCoordinate, TNodeData, TEdgeData> world;

		public Configured(BuildingWorld<TCoordinate, TNodeData, TEdgeData> buildingWorld)
		{
			world = buildingWorld.Build();
			mapModifier = world.GetModifier();
		}

		#region Implementation of IConfigured<TCoordinate,TNodeData,TEdgeData>

		public IConfigured<TCoordinate, TNodeData, TEdgeData> SeedData(Action<IEdgesDataLayer<TCoordinate, TEdgeData>> seed)
		{
			seed(world.EdgesData);
			return this;
		}

		public IConfigured<TCoordinate, TNodeData, TEdgeData> SeedData(Action<INodesDataLayer<TCoordinate, TNodeData>> seed)
		{
			seed(world.NodesData);
			return this;
		}

		public IConfigured<TCoordinate, TNodeData, TEdgeData> SeedAnts(Action<IMapModifier<TCoordinate, TNodeData, TEdgeData>> seed)
		{
			seed(mapModifier);
			return this;
		}

		public World<TCoordinate, TNodeData, TEdgeData> Build()
		{
			mapModifier.Dispose();
			return world;
		}

		#endregion
	}
}