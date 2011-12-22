﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILibrary.General;
using SILibrary.General.Background;
using SILibrary.Graph;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.Logging;

namespace Example2
{
    class Programnew
    {
        private static World<GraphCoordinate, EmptyData, DictionaryDataLayer<Edge<GraphCoordinate>, double>> _world;
        private static Runner<GraphCoordinate, EmptyData, DictionaryDataLayer<Edge<GraphCoordinate>, double>> _runner;
        private static ILogJournal _logger;

        public static void Main(string[] args)
        {
            Tuple<Runner<GraphCoordinate, EmptyData, DictionaryDataLayer<Edge<GraphCoordinate>, double>>, ILogJournal>
                tuple = SystemBuilder
                    .Create<GraphCoordinate, EmptyData, DictionaryDataLayer<Edge<GraphCoordinate>, double>>()
                    .WithTopology(new GraphTopology(null))
                    .Build();

            _runner = tuple.Item1;
            _logger = tuple.Item2;
            _world = _runner.World;
        }
    }
}
