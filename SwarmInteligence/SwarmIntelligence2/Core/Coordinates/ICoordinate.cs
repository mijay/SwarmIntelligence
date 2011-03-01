﻿using System;

namespace SwarmIntelligence2.Core.Coordinates
{
    /// <summary>
    /// Interface for representing coordinates on the <see cref="Map{C,B}"/>.
    /// It should be structure.
    /// </summary>
    public interface ICoordinate<C>: ICloneable, IEquatable<C>
        where C: ICoordinate<C>
    {
        new C Clone();
    }
}