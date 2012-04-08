using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common.Collections.Extensions;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.BuildUp
{
	public static class EntityBuilders<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		public static CellBuilder<TCoordinate, TNodeData, TEdgeData> ForCell<TCell>()
			where TCell: CellBase<TCoordinate, TNodeData, TEdgeData>
		{
			Type mapType = typeof(Map<TCoordinate, TNodeData, TEdgeData>);
			Type coordinateType = typeof(TCoordinate);
			ConstructorInfo constructor = typeof(TCell).GetConstructor(new[] { mapType, coordinateType });
			ParameterExpression xMap = Expression.Parameter(mapType);
			ParameterExpression xCoordinate = Expression.Parameter(coordinateType);
			return Expression
				.Lambda<CellBuilder<TCoordinate, TNodeData, TEdgeData>>(
					Expression.TypeAs(
						Expression.New(constructor, xMap, xCoordinate),
						typeof(CellBase<TCoordinate, TNodeData, TEdgeData>)),
					xMap, xCoordinate)
				.Compile();
		}

		internal static IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>> ForStorage<TStorage>(
			Topology<TCoordinate> topology)
			where TStorage: IValueStorage<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		{
			var constructors = typeof(TStorage)
				.GetConstructors()
				.Select(x => new { Constructor = x, Params = x.GetParameters() })
				.ToArray();

			ConstructorInfo defaultConstructor;
			if(constructors
				.Where(x => x.Params.Length == 0)
				.Select(x => x.Constructor)
				.TrySingle(out defaultConstructor))
				return (TStorage) defaultConstructor.Invoke(new object[0]);

			ConstructorInfo topologyBasedConsrtuctor = constructors
				.Where(x => x.Params.Length == 1 && x.Params[0].ParameterType.IsAssignableFrom(topology.GetType()))
				.Select(x => x.Constructor)
				.Single();
			return (TStorage) topologyBasedConsrtuctor.Invoke(new object[] { topology });
		}
	}
}