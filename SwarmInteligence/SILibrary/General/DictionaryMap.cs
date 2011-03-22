using System.Collections.Concurrent;
using System.Collections.Generic;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Core.Space;

namespace SILibrary.General
{
    public class DictionaryMap<C, B, E>: Map<C, B, E>
        where C: ICoordinate<C>
    {
        public readonly ConcurrentDictionary<C, Cell<C, B, E>> data = new ConcurrentDictionary<C, Cell<C, B, E>>();

        #region Overrides of Map<C,B>

        public DictionaryMap(Boundaries<C> boundaries): base(boundaries) {}

        public override Cell<C, B, E> this[C key]
        {
            get { return data.GetOrAdd(key, delegate { return new Cell<C, B, E>(); }); }
        }

        public override bool IsInitialized(C key)
        {
            return data.ContainsKey(key);
        }

        public override void Free(C key)
        {
            Cell<C, B, E> cell;
            data.TryRemove(key, out cell);
        }

        public override IEnumerable<KeyValuePair<C, Cell<C, B, E>>> GetInitialized()
        {
            return data;
        }

        #endregion
    }
}