using System;
using System.Collections.Generic;
using SILibrary.Graph;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.MemoryManagement;

namespace HamiltonianCycle
{
    class EdgeValueProvider : IValueProvider<Edge<GraphCoordinate>, Tuple<Edge<GraphCoordinate>, EdgeData>>
    {
        Dictionary<Edge<GraphCoordinate>, EdgeData> dataEdges = new Dictionary<Edge<GraphCoordinate>, EdgeData>();

        public void Return(Tuple<Edge<GraphCoordinate>, EdgeData> cell)
        {
            dataEdges.Add(cell.Item1, cell.Item2);
        }

        public Tuple<Edge<GraphCoordinate>, EdgeData> Get(Edge<GraphCoordinate> key)
        {
            EdgeData edgeData;
            dataEdges.TryGetValue(key, out edgeData);
            return new Tuple<Edge<GraphCoordinate>, EdgeData>(key, edgeData);
        }
    }
}
