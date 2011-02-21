using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SwarmIntelligence2.Core
{
    public class Cell<C, B>: IEnumerable<Ant<C, B>>
        where C: struct, Interface.ICoordinate<C>
    {
        private readonly List<Ant<C, B>> list = new List<Ant<C, B>>();

        public void Add(Ant<C, B> ant)
        {
            Contract.Requires(ant != null);
            Contract.Ensures(this.Contains(ant));

            list.Add(ant);
        }

        public void Remove(Ant<C, B> ant)
        {
            Contract.Requires(ant != null);
            Contract.Requires(this.Contains(ant));
            Contract.Ensures(!this.Contains(ant));

            list.Remove(ant);
        }

        #region Implementation of IEnumerable

        public IEnumerator<Ant<C, B>> GetEnumerator()
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