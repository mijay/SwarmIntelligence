using System.Collections.Concurrent;
using System.Collections.Generic;
using SwarmIntelligence2.Core;
using SwarmIntelligence2.Core.Coordinates;
using SwarmIntelligence2.Core.World;

namespace SwarmIntelligence2.GeneralImplementation
{
    public class DictionaryMap<C, B>: Map<C, B>
        where C: ICoordinate<C>
    {
        public readonly ConcurrentDictionary<C, Cell<C, B>> data = new ConcurrentDictionary<C, Cell<C, B>>();

        #region Overrides of Map<C,B>

        public DictionaryMap(Range<C> range): base(range) {}

        public override Cell<C, B> this[C key]
        {
            get { return data.GetOrAdd(key, delegate { return new Cell<C, B>(); }); }
        }

        public override bool IsInitialized(C key)
        {
            return data.ContainsKey(key);
        }

        public override void Free(C key)
        {
            Cell<C, B> cell;
            data.TryRemove(key, out cell);
        }

        public override IEnumerable<KeyValuePair<C, Cell<C, B>>> GetInitialized()
        {
            return data;
        }

        #endregion
    }
}