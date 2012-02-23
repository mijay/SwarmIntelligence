using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation;
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

		internal static MappingBuilder<TCoordinate, TNodeData, TEdgeData> ForMapping<TMapping>(
			SystemBuilder.CellProviderBuilder<TCoordinate, TNodeData, TEdgeData> cellProviderBuilder,
			Topology<TCoordinate> topology, ILog log)
			where TMapping: MappingBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		{
			ConstructorInfo constructorInfo = typeof(TMapping).GetConstructors().Single();

			ParameterExpression xParameter = Expression.Parameter(typeof(Map<TCoordinate, TNodeData, TEdgeData>));

			Expression[] xConstructorArguments = constructorInfo.GetParameters()
				.Select(parameterInfo => {
				        	if(parameterInfo.ParameterType.IsAssignableFrom(cellProviderBuilder.GetType()))
				        		return (Expression) Expression.Constant(cellProviderBuilder);
				        	if(parameterInfo.ParameterType.IsAssignableFrom(topology.GetType()))
				        		return Expression.Constant(topology);
				        	if(parameterInfo.ParameterType.IsAssignableFrom(log.GetType()))
				        		return Expression.Constant(log);

				        	if(parameterInfo.ParameterType.IsAssignableFrom(
				        		typeof(IValueProvider<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>)))
				        		return Expression.Invoke(Expression.Constant(cellProviderBuilder), xParameter);
				        	if(parameterInfo.ParameterType.IsAssignableFrom(typeof(Map<TCoordinate, TNodeData, TEdgeData>)))
				        		return xParameter;
				        	throw new ArgumentOutOfRangeException();
				        })
				.ToArray();

			return Expression
				.Lambda<MappingBuilder<TCoordinate, TNodeData, TEdgeData>>(
					Expression.New(constructorInfo, xConstructorArguments),
					xParameter)
				.Compile();
		}
	}
}