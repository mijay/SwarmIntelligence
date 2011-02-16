using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.Core
{
    public abstract class Background<C, B>: ICoordinateMapping<C, B>
        where C: struct, Interface.ICoordinate<C>
    {
        #region Implementation of ICoordinateMapping<C,out B>

        public abstract B this[C coord] { get; }

        #endregion
    }
}