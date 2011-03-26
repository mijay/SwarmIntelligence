using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Common;
using Common.Cache;
using Common.Collections;
using CommonTest;
using Ninject;
using NUnit.Framework;
using Ninject.Extensions.Conventions;
using SILibrary.General;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Infrastructure.Commands;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;
using SwarmIntelligence.Infrastructure.Implementation;

namespace Test.ContextIndependendAnt
{
    public class Test: TestBase
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();
            random = new Random();

            var boundaries = new Boundaries2D(min, max);
            var topology = new EightConnectedSurfaceTopology(boundaries);
            var map = new DictionaryMap<Coordinates2D, EmptyData, EmptyData>(boundaries);
            var nodeDataLayer = new EmptyNodeDataLayer<Coordinates2D>(boundaries);
            var edgeDataLayer = new EmptyEdgeDataLayer<Coordinates2D>(topology);

            world = new World<Coordinates2D, EmptyData, EmptyData>(boundaries, topology, nodeDataLayer, edgeDataLayer, map);

            var kernel = new StandardKernel(new NinjectSettings
                                            {
                                                ActivationCacheDisabled = false,
                                                AllowNullInjection = false,
                                                UseReflectionBasedInjection = true
                                            });
            kernel.Scan(x => {
                            x.From(AppDomain.CurrentDomain.GetAssemblies());
                            x.InSingletonScope();
                            x.BindWith(new AllInterfacesBinding());
                            //x.BindWithDefaultConventions();
                        });

            kernel.Rebind<IKeyValueCache>().To<LocalCache>().InTransientScope();

            runner = new Runner<Coordinates2D, EmptyData, EmptyData>(world, kernel.Get<ICommandDispatcher<Coordinates2D, EmptyData, EmptyData>>());
        }

        #endregion

        public Test(): this(-4, -3, 12, 5) {}

        public Test(int minX, int minY, int maxX, int maxY)
        {
            min = new Coordinates2D(minX, minY);
            max = new Coordinates2D(maxX, maxY);
        }

        private World<Coordinates2D, EmptyData, EmptyData> world;
        private Random random;
        private Runner<Coordinates2D, EmptyData, EmptyData> runner;
        private readonly Coordinates2D min;
        private readonly Coordinates2D max;

        private IEnumerable<TestAnt> SeedAnts(int antsNumber, int timesAntJumps, params Coordinates2D[] lastAntSteps)
        {
            return EnumerableExtension
                .Repeat(() => SeedAnt(timesAntJumps, lastAntSteps), antsNumber)
                .ToArray();
        }

        private TestAnt SeedAnt(int timesAntJumps, params Coordinates2D[] lastAntSteps)
        {
            Coordinates2D initialPosition = GenerateCoordinate();
            var testAnt = new TestAnt(EnumerableExtension
                                          .Repeat(GenerateCoordinate, timesAntJumps)
                                          .Concat(lastAntSteps)
                                          .ToArray());
            world.Map[initialPosition].Add(testAnt);
            return testAnt;
        }

        private Coordinates2D GenerateCoordinate()
        {
            int x = random.Next(min.x, max.x + 1);
            int y = random.Next(min.y, max.y + 1);
            return new Coordinates2D(x, y);
        }

        [Test]
        public void SimpleTest([Values(5, 10, 20, 80, 200)] int jumps, [Values(100, 1000)] int ants)
        {
            Coordinates2D lastStep = GenerateCoordinate();
            IEnumerable<TestAnt> seededAnts = SeedAnts(ants, jumps - 1, lastStep);
            var timer = new Stopwatch();
            timer.Start();
            for(int i = 0; i < jumps; ++i)
                runner.ProcessTurn();
            timer.Stop();
            Console.WriteLine(string.Format("jumps - {0}; ants - {1}; time - {2} ms", jumps, ants, timer.ElapsedMilliseconds));

            KeyValuePair<Coordinates2D, Cell<Coordinates2D, EmptyData, EmptyData>> keyValuePair = world.Map.GetInitialized().Single();
            Assert.That(keyValuePair.Key, Is.EqualTo(lastStep));
            CollectionAssert.AreEquivalent(seededAnts, keyValuePair.Value);
        }
    }
}