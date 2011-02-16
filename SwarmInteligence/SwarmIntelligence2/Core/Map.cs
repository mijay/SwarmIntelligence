using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.Core
{
    public abstract class Map<C, B>: IIncompleteCoordinateMapping<C, Cell<C, B>>
        where C: struct, Interface.ICoordinate<C>
    {
        #region Implementation of ICoordinateMapping<C,out Cell<C,B>>

        public abstract Cell<C, B> this[C coord] { get; }

        #endregion

        #region Implementation of IIncompleteCoordinateMapping<C,out Cell<C,B>>

        public abstract bool HasData(C coord);

        #endregion
    }
}