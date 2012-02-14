using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Infrastructure.MemoryManagement;
using SwarmIntelligence.Infrastructure.Playground;
using SwarmIntelligence.Internal;

namespace SwarmIntelligence.Specialized
{
	public static class MapModifierHelper
	{
		public static IMapModifier<TCoordinate, TNodeData, TEdgeData> GetModifier<TCoordinate, TNodeData, TEdgeData>(
			this World<TCoordinate, TNodeData, TEdgeData> world)
			where TCoordinate: ICoordinate<TCoordinate>
		{
			Contract.Requires(world != null);
			return new MapModifier<TCoordinate, TNodeData, TEdgeData>(world.Map.Base(), world.Log);
		}

		#region Nested type: MapModifier

		private class MapModifier<TCoordinate, TNodeData, TEdgeData>: DisposableBase, IMapModifier<TCoordinate, TNodeData, TEdgeData>
			where TCoordinate: ICoordinate<TCoordinate>
		{
			private static readonly HashSet<object> alreadyModifiedMaps = new HashSet<object>();
			private readonly ILog log;
			private readonly Map<TCoordinate, TNodeData, TEdgeData> map;

			public MapModifier(Map<TCoordinate, TNodeData, TEdgeData> map, ILog log)
			{
				Contract.Requires(map != null && log != null);
				Requires.True(alreadyModifiedMaps.Add(map));
				this.map = map;
				this.log = log;
			}

			#region Implementation of IMutableMap<TCoordinate,TNodeData,TEdgeData>

			public IMap<TCoordinate, TNodeData, TEdgeData> Map
			{
				get { return map; }
			}

			public void AddAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
			{
				map.Get(coordinate).Add(ant);
				log.Log(CommonLogTypes.AntAdded, ant, coordinate);
			}

			public void RemoveAt(IAnt<TCoordinate, TNodeData, TEdgeData> ant, TCoordinate coordinate)
			{
				map.Get(coordinate).Remove(ant);
				log.Log(CommonLogTypes.AntRemoved, ant, coordinate);
			}

			#endregion
		}

		#endregion
	}
}