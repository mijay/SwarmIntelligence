using System;
using SwarmIntelligence.Core.Creatures;

namespace SwarmIntelligence.Core
{
    /// <summary>
    /// Interface for representing coordinates on the <see cref="Map{C,B,E}"/>.
    /// It should be structure.
    /// </summary>
    public interface ICoordinate<C>: ICloneable, IEquatable<C>
        where C: ICoordinate<C>
    {
        new C Clone();
    }
}