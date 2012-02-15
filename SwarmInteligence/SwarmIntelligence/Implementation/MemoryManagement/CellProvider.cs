using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Implementation.Playground;

namespace SwarmIntelligence.Implementation.MemoryManagement
{
	public class CellProvider<TCoordinate, TNodeData, TEdgeData>:
		ReusingValueProviderBase<TCoordinate, CellBase<TCoordinate, TNodeData, TEdgeData>>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		#region Delegates

		public delegate CellBase<TCoordinate, TNodeData, TEdgeData> Builder(Map<TCoordinate, TNodeData, TEdgeData> map, TCoordinate coordinate);

		#endregion

		private readonly Builder builder;
		private readonly Map<TCoordinate, TNodeData, TEdgeData> map;

		public CellProvider(Map<TCoordinate, TNodeData, TEdgeData> map, Builder builder)
		{
			Contract.Requires(map != null && builder != null);
			this.map = map;
			this.builder = builder;
		}

		#region Overrides of ReusingValueProviderBase<TCoordinate,CellBase<TCoordinate,TNodeData,TEdgeData>>

		protected override CellBase<TCoordinate, TNodeData, TEdgeData> Create(TCoordinate key)
		{
			return builder(map, key);
		}

		protected override void Modify(CellBase<TCoordinate, TNodeData, TEdgeData> value, TCoordinate newKey)
		{
			value.Coordinate = newKey;
		}

		#endregion

		public static Builder BuilderFor<TCell>()
			where TCell: CellBase<TCoordinate, TNodeData, TEdgeData>
		{
			Type mapType = typeof(Map<TCoordinate, TNodeData, TEdgeData>);
			Type coordinateType = typeof(TCoordinate);
			ConstructorInfo constructor = typeof(TCell).GetConstructor(new[] { mapType, coordinateType });
			ParameterExpression xMap = Expression.Parameter(mapType);
			ParameterExpression xCoordinate = Expression.Parameter(coordinateType);
			return Expression
				.Lambda<Builder>(
					Expression.TypeAs(
						Expression.New(constructor, xMap, xCoordinate),
						typeof(CellBase<TCoordinate, TNodeData, TEdgeData>)),
					xMap, xCoordinate)
				.Compile();
		}
	}
}