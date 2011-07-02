using System.Collections.Generic;
using System.Linq;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Core.PlayingField;
using SwarmIntelligence.Core.Space;
using Common.Collections;

namespace SwarmIntelligence.Infrastructure
{
    public class Runner<C, B, E>
        where C: ICoordinate<C>
    {
        private readonly World<C, B, E> world;

        public Runner(World<C, B, E> world)
        {
            this.world = world;
        }

        public void ProcessTurn()
        {
            var outlook = new ThreadLocalOutlook<C,B,E>(world);
            world.Map
                .GetInitialized()
                .AsParallel()
                .SelectMany(GetAntContext)
                .ToArray()
                .ForEach(x => {
                             outlook.Coordinate = x.coord;
                             outlook.Cell = x.cell;
                             outlook.Me = x.ant;
                             x.ant.ProcessTurn(outlook);
                         });
        }

        private static IEnumerable<AntContext> GetAntContext(KeyValuePair<C, Cell<C, B, E>> cellWithCoord)
        {
            return cellWithCoord.Value
                .Select(ant => new AntContext
                               {
                                   coord = cellWithCoord.Key,
                                   cell = cellWithCoord.Value,
                                   ant = ant
                               });
        }

        #region Nested type: AntContext

        private struct AntContext
        {
            public Ant<C, B, E> ant;
            public C coord;
            public Cell<C, B, E> cell;
        }

        #endregion
    }
}