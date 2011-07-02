using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Common.Cache;
using Common.Collections;
using CommonTest;
using NUnit.Framework;
using SILibrary.General;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using StructureMap;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
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

            var topology = new EightConnectedSurfaceTopology(min, max);
            var map = new DictionaryMap<Coordinates2D, EmptyData, EmptyData>(topology);
            var nodeDataLayer = new EmptyNodeDataLayer<Coordinates2D>(topology);
            var edgeDataLayer = new EmptyEdgeDataLayer<Coordinates2D>(topology);

            world = new World<Coordinates2D, EmptyData, EmptyData>(topology, nodeDataLayer, edgeDataLayer, map);

            var container = new Container(x => {
                                              x.Scan(a => {
                                                         a.Assembly(Assembly.Load("Common"));
                                                         a.Assembly(Assembly.Load("SwarmIntelligence"));
                                                         a.Assembly(Assembly.Load("SILibrary"));
                                                         a.WithDefaultConventions();
                                                     });
                                              x.For<IKeyValueCache>().AlwaysUnique().Use<LocalCache>();
                                          });
            runner = new Runner<Coordinates2D, EmptyData, EmptyData>(
                world,container.GetInstance<ICommandDispatcher<Coordinates2D, EmptyData, EmptyData>>());
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
            Debug.WriteLine(string.Format("jumps - {0}; ants - {1}; time - {2} ms", jumps, ants, timer.ElapsedMilliseconds));

            KeyValuePair<Coordinates2D, Cell<Coordinates2D, EmptyData, EmptyData>> keyValuePair =
                world.Map.GetInitialized().Single();
            Assert.That(keyValuePair.Key, Is.EqualTo(lastStep));
            CollectionAssert.AreEquivalent(seededAnts, keyValuePair.Value);
        }
    }
}