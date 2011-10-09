using System;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.Common;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.GrabgeCollection;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Specialized;

namespace Example1
{
    class Program
    {
        private static World<Coordinates2D, EmptyData, EmptyData> _world;
        private static Runner<Coordinates2D, EmptyData, EmptyData> _runner;
        private static readonly Coordinates2D Min = new Coordinates2D(-10, -10);
        private static readonly Coordinates2D Max = new Coordinates2D(10, 10);

        private static readonly Random Random = new Random();


        static void Main(string[] args)
        {
            var logger = new LogManager();
            var topology = new EightConnectedSurfaceTopology(Min, Max);
            var cellProvider = SetCell<Coordinates2D, EmptyData, EmptyData>.Provider();
            var map = new DictionaryMap<Coordinates2D, EmptyData, EmptyData>(topology, cellProvider, logger.Log);
            var nodeDataLayer = new EmptyDataLayer<Coordinates2D>();
            var edgeDataLayer = new EmptyDataLayer<Edge<Coordinates2D>>();

            _world = new World<Coordinates2D, EmptyData, EmptyData>(nodeDataLayer, edgeDataLayer, map, logger.Log);
            _runner = new Runner<Coordinates2D, EmptyData, EmptyData>(_world, new GarbageCollector<Coordinates2D, EmptyData, EmptyData>());

            var ants = SeedAnts(10);

            for (var i = 0; i < 10; i++)
            {
                _runner.DoTurn();
            }
        }

        private static IAnt<Coordinates2D, EmptyData, EmptyData>[] SeedAnts(int count)
        {
            using (var mapModifier = _world.Map.GetModifier())
                return EnumerableExtension.Repeat(() => SeedAnt(mapModifier), count).ToArray();
        }

        private static Coordinates2D GenerateCoordinates()
        {
            var x = Random.Next(-10, 10);
            var y = Random.Next(-10, 10);
            return new Coordinates2D(x, y);
        }

        private static IAnt<Coordinates2D, EmptyData, EmptyData> SeedAnt(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier)
        {
            var initialCoordinates = GenerateCoordinates();
            var ant = Random.NextDouble() > 0.5 ? (IAnt<Coordinates2D, EmptyData, EmptyData>) new WolfAnt(_world) : new PreyAnt(_world);
            mapModifier.AddAt(ant, initialCoordinates);
            return ant;
        }
    }
}
