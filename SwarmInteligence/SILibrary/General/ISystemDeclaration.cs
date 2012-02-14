using System;
using SwarmIntelligence;
using SwarmIntelligence.Core.Data;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.Logging;

namespace SILibrary.General
{
	public interface ISystemDeclaration<TCoordinate, TNodeData, TEdgeData>
		where TCoordinate: ICoordinate<TCoordinate>
	{
		ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithTopology(Topology<TCoordinate> topology);
		ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithEdgeData(IEdgesDataLayer<TCoordinate, TEdgeData> dataLayer);
		ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithNodeData(INodesDataLayer<TCoordinate, TNodeData> dataLayer);
		Tuple<Runner<TCoordinate, TNodeData, TEdgeData>, ILogJournal> Build();
	}
}