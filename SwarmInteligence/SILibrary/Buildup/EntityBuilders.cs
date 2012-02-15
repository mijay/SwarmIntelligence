using System;
using System.Linq.Expressions;
using System.Reflection;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation;
using SwarmIntelligence.Implementation.MemoryManagement;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.Buildup
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

		public static MappingBuilder<TCoordinate, TNodeData, TEdgeData> ForMapMapping<TMapping, TCell>(ILog log)
			where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
			where TCell: CellBase<TCoordinate, TNodeData, TEdgeData>
		{
			return map => {
			       	var cellProvider = new CellProvider<TCoordinate, TNodeData, TEdgeData>(map, ForCell<TCell>());
			       	return (TMapping) Activator.CreateInstance(typeof(TMapping), cellProvider, log);
			       };
		}
	}
}