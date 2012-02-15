﻿using System;
using CommonTest;
using SILibrary.Buildup;
using SILibrary.Empty;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Implementation.Logging;

namespace Test.SwarmInteligence
{
	public abstract class SwarmIntelligenceTestBase: TestBase
	{
		protected World<Coordinates2D, EmptyData, EmptyData> world;
		protected Runner<Coordinates2D, EmptyData, EmptyData> runner;
		protected ILogJournal journal;

		protected void InitializeWorld(Coordinates2D min, Coordinates2D max)
		{
			Tuple<Runner<Coordinates2D, EmptyData, EmptyData>, ILogJournal> tuple = SystemBuilder
				.Create<Coordinates2D, EmptyData, EmptyData>()
				.WithTopology(new FourConnectedSurfaceTopology(min, max))
				.Build();
			runner = tuple.Item1;
			journal = tuple.Item2;
			world = runner.World;
		}
	}
}