using Common;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence.Specialized
{
	public static class MapModifierHelper
	{
		public static IMapModifier<TCoordinate, TNodeData, TEdgeData> GetModifier<TCoordinate, TNodeData, TEdgeData>(
			this IMap<TCoordinate, TNodeData, TEdgeData> map)
		{
			Requires.NotNull(map);
			return new MapModifier<TCoordinate, TNodeData, TEdgeData>(map.Base());
		}

		#region Nested type: MapModifier

		private class MapModifier<TCoordinate, TNodeData, TEdgeData>: DisposableBase, IMapModifier<TCoordinate, TNodeData, TEdgeData>
		{
			private readonly MapBase<TCoordinate, TNodeData, TEdgeData> mapBase;

			public MapModifier(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase)
			{
				this.mapBase = mapBase;
			}

			#region Implementation of IMutableMap<TCoordinate,TNodeData,TEdgeData>

			public void AddAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
			{
				Requires.NotNull(ant);
				mapBase.Get(coordinate).Base().Add(ant);
			}

			public void RemoveAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
			{
				Requires.NotNull(ant);
				mapBase.Get(coordinate).Base().Remove(ant);
			}

			#endregion

			protected override void DisposeManaged()
			{
				//todo: implement!
			}
		}

		#endregion
	}
}