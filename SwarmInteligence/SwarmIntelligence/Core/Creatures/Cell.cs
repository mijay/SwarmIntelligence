using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SwarmIntelligence.Core.Creatures
{
    public class Cell<C, B, E>: IEnumerable<Ant<C, B, E>>
        where C: ICoordinate<C>
    {
        private readonly List<Ant<C, B, E>> list = new List<Ant<C, B, E>>();

        public void Add(Ant<C, B, E> ant)
        {
            Contract.Requires(ant != null);
            Contract.Requires(!this.Contains(ant));
            Contract.Ensures(this.Contains(ant));

            list.Add(ant);
        }

        public void Remove(Ant<C, B, E> ant)
        {
            Contract.Requires(ant != null);
            Contract.Requires(this.Contains(ant));
            Contract.Ensures(!this.Contains(ant));

            list.Remove(ant);
        }

        #region Implementation of IEnumerable

        public IEnumerator<Ant<C, B, E>> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}