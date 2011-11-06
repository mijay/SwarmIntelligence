﻿using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.Logging;

namespace SILibrary.General
{
	public interface ISystemDeclaration<TCoordinate, TNodeData, TEdgeData>
	{
		ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithTopology(Topology<TCoordinate> topology);
		ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithEdgeData(DataLayer<Edge<TCoordinate>, TEdgeData> dataLayer);
		ISystemDeclaration<TCoordinate, TNodeData, TEdgeData> WithNodeData(DataLayer<TCoordinate, TNodeData> dataLayer);
		Runner<TCoordinate, TNodeData, TEdgeData> Build(out ILogJournal logJournal);
	}
}