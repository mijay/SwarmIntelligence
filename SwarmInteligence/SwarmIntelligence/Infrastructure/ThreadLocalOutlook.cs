using System.Threading;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Core.PlayingField;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure
{
    public class ThreadLocalOutlook<C, B, E>: IOutlook<C, B, E>
        where C: ICoordinate<C>
    {
        private readonly ThreadLocal<Ant<C, B, E>> ant = new ThreadLocal<Ant<C, B, E>>();
        private readonly ThreadLocal<Cell<C, B, E>> cell = new ThreadLocal<Cell<C, B, E>>();
        private readonly ThreadLocal<C> coordinate = new ThreadLocal<C>();

        public ThreadLocalOutlook(World<C, B, E> world)
        {
            World = world;
        }

        #region Implementation of IOutlook<C,B,E>

        public World<C, B, E> World { get; private set; }

        public C Coordinate
        {
            get { return coordinate.Value; }
            set { coordinate.Value = value; }
        }

        public Cell<C, B, E> Cell
        {
            get { return cell.Value; }
            set { cell.Value = value; }
        }

        public Ant<C, B, E> Me
        {
            get { return ant.Value; }
            set { ant.Value = value; }
        }

        #endregion
    }
}