using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence.Specialized
{
	public static class MapModifierHelper
	{
		public static IMapModifier<TCoordinate, TNodeData, TEdgeData> GetModifier<TCoordinate, TNodeData, TEdgeData>(
			this World<TCoordinate, TNodeData, TEdgeData> world)
		{
			Contract.Requires(world != null);
			return new MapModifier<TCoordinate, TNodeData, TEdgeData>(world.Map.Base(), world.Log);
		}

		#region Nested type: MapModifier

		private class MapModifier<TCoordinate, TNodeData, TEdgeData>: DisposableBase, IMapModifier<TCoordinate, TNodeData, TEdgeData>
		{
			private static readonly HashSet<object> alreadyModifiedMaps = new HashSet<object>();
			private readonly ILog log;
			private readonly MapBase<TCoordinate, TNodeData, TEdgeData> mapBase;

			public MapModifier(MapBase<TCoordinate, TNodeData, TEdgeData> mapBase, ILog log)
			{
				Contract.Requires(mapBase != null && log != null);
				Requires.True(alreadyModifiedMaps.Add(mapBase));
				this.mapBase = mapBase;
				this.log = log;
			}

			#region Implementation of IMutableMap<TCoordinate,TNodeData,TEdgeData>

			public IMap<TCoordinate, TNodeData, TEdgeData> Map
			{
				get { return mapBase; }
			}

			public void AddAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
			{
				mapBase.Get(coordinate).Base().Add(ant);
				log.Log(CommonLogTypes.AntAdded, ant, coordinate);
			}

			public void RemoveAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
			{
				mapBase.Get(coordinate).Base().Remove(ant);
				log.Log(CommonLogTypes.AntRemoved, ant, coordinate);
			}

			#endregion
		}

		#endregion
	}
}