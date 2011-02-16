using System.Collections.Concurrent;
using SwarmIntelligence2.Core;
using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.GeneralImplementation
{
    public class DictionaryMap<C, B>: Map<C, B>
        where C: struct, ICoordinate<C>
    {
        public readonly ConcurrentDictionary<C, Cell<C, B>> data = new ConcurrentDictionary<C, Cell<C, B>>();

        #region Overrides of Map<C,B>

        public override Cell<C, B> this[C coord]
        {
            get { return data.GetOrAdd(coord, delegate { return new Cell<C, B>(); }); }
        }

        public override bool HasData(C coord)
        {
            return data.ContainsKey(coord);
        }

        #endregion
    }
}