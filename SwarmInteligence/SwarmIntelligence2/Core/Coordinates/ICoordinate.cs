using System;

namespace SwarmIntelligence2.Core.Coordinates
{
    /// <summary>
    /// Interface for representing coordinates on the <see cref="Map{C,B}"/>.
    /// </summary>
    public interface ICoordinate<C>: ICloneable, IEquatable<C>
        where C: struct, ICoordinate<C>
    {
        new C Clone();
    }
}