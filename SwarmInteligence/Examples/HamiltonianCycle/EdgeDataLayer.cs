using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILibrary.Common;
using SILibrary.Graph;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.MemoryManagement;

namespace HamiltonianCycle
{
    class EdgeDataLayer : MappingBase<Edge<GraphCoordinate>, EdgeData>
    {
        ConcurrentDictionary<Edge<GraphCoordinate>, EdgeData> edges = new ConcurrentDictionary<Edge<GraphCoordinate>, EdgeData>();

        public EdgeDataLayer(IValueProvider<Edge<GraphCoordinate>, EdgeData> valueProvider, ILog log) : base(valueProvider, log)
        {
        }

        public override bool TryGet(Edge<GraphCoordinate> key, out EdgeData value)
        {
            return edges.TryGetValue(key, out value);
        }

        public override IEnumerator<KeyValuePair<Edge<GraphCoordinate>, EdgeData>> GetEnumerator()
        {
            return edges.GetEnumerator();
        }

        protected override bool TryRemove(Edge<GraphCoordinate> key, out EdgeData value)
        {
            return edges.TryRemove(key, out value);
        }

        protected override EdgeData GetOrAdd(Edge<GraphCoordinate> key, EdgeData value)
        {
            return edges.GetOrAdd(key, value);
        }
    }
}
