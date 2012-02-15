using System;
using SwarmIntelligence;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.Logging;

namespace SILibrary.Buildup
{
	public interface ISystemDeclaration<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithTopology(Topology<TCoordinate> topology);

		ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithEdgeData(
			Func<Topology<TCoordinate>, IEdgesDataLayer<TCoordinate, TEdgeData>> dataLayerBuilder);

		ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithNodeData(
			Func<Topology<TCoordinate>, INodesDataLayer<TCoordinate, TNodeData>> dataLayerBuilder);

		Tuple<Runner<TCoordinate, TNodeData, TEdgeData>, ILogJournal> Build();
	}
}