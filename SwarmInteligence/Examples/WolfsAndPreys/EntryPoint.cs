﻿using System;
using System.Linq;
using Common.Collections.Extensions;
using SILibrary.BuildUp;
using SILibrary.Empty;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Specialized;

namespace WolfsAndPreys
{
	public static class EntryPoint
	{
		private static World<Coordinates2D, EmptyData, EmptyData> world;
		private static Runner<Coordinates2D, EmptyData, EmptyData> runner;
		private static readonly Coordinates2D min = new Coordinates2D(-10, -10);
		private static readonly Coordinates2D max = new Coordinates2D(10, 10);

		private static readonly Random random = new Random();

		private static void Main(string[] args)
		{
			ILogManager logManager;
			world = SystemBuilder
				.Create<Coordinates2D, EmptyData, EmptyData>()
				.WithDefaultLog(out logManager)
				.WithTopology(new EightConnectedSurfaceTopology(min, max))
				.WithSurfaceMap()
				.WithEmptyNodeData()
				.WithEmptyEdgeData()
				.Build();
			runner = new Runner<Coordinates2D, EmptyData, EmptyData>(world);

			IAnt<Coordinates2D, EmptyData, EmptyData>[] ants = SeedAnts(10);

			for(int i = 0; i < 10; i++)
				runner.DoTurn();
		}

		private static IAnt<Coordinates2D, EmptyData, EmptyData>[] SeedAnts(int count)
		{
			using(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier = world.GetModifier())
				return EnumerableExtension.Repeat(() => SeedAnt(mapModifier), count).ToArray();
		}

		private static Coordinates2D GenerateCoordinates()
		{
			int x = random.Next(0, 10);
			int y = random.Next(0, 10);
			return new Coordinates2D(x, y);
		}

		private static IAnt<Coordinates2D, EmptyData, EmptyData> SeedAnt(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier)
		{
			Coordinates2D initialCoordinates = GenerateCoordinates();
			IAnt<Coordinates2D, EmptyData, EmptyData> ant = random.NextDouble() > 0.5
			                                                	? (IAnt<Coordinates2D, EmptyData, EmptyData>) new WolfAnt(world)
			                                                	: new PreyAnt(world);
			mapModifier.AddAt(ant, initialCoordinates);
			return ant;
		}
	}
}