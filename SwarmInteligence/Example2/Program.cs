using SILibrary.Common;
using SILibrary.General.Background;
using SILibrary.Graph;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.GrabgeCollection;
using SwarmIntelligence.Infrastructure.Logging;

namespace Example2
{
	internal class Program
	{
	    private static World<GraphCoordinate, EmptyData, OdorData> _world;
	    private static Runner<GraphCoordinate, EmptyData, OdorData> _runner;

		private static void Main(string[] args)
		{
		    var logger = new LogManager();
		    var topology = new GraphTopology(null, null);
		    var cellProvider = SetCell<GraphCoordinate, EmptyData, OdorData>.Provider();
		    var map = new DictionaryMap<GraphCoordinate, EmptyData, OdorData>(topology, cellProvider, logger.Log);
		    var nodeDataLayer = new EmptyDataLayer<GraphCoordinate>();
		    var edgeDataLayer = new OdorDataLayer<Edge<GraphCoordinate>>();

            _world = new World<GraphCoordinate, EmptyData, OdorData>(nodeDataLayer, edgeDataLayer, map, logger.Log);
            _runner = new Runner<GraphCoordinate, EmptyData, OdorData>(_world, new GarbageCollector<GraphCoordinate, EmptyData, OdorData>());
		}
	}
}