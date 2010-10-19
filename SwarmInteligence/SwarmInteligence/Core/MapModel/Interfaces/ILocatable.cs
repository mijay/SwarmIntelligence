using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    /// <summary>
    /// Represents all objects that can be placed on the <see cref="District{C,B}"/>
    /// </summary>
    [ContractClass(typeof(LocatableContract<,>))]
    public interface ILocatable<C, B>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// Coordinate of the object on the <see cref="Map{C,B}"/>
        /// </summary>
        [Pure]
        C Coordinate { get; }

        [Pure]
        District<C, B> District { get; }
    }


    [ContractClassFor(typeof(ILocatable<,>))]
    internal class LocatableContract<C, B>: ILocatable<C, B>
        where C: struct, ICoordinate<C>
    {
        #region ILocatable<C,B> Members

        public C Coordinate
        {
            get
            {
                Contract.Ensures(Contract.Result<C>().IsInRange(District.Bounds.Item1, District.Bounds.Item2),
                                 "IComponent belongs to the wrong District");
                throw new NotImplementedException();
            }
        }

        public District<C, B> District
        {
            get
            {
                Contract.Ensures(Contract.Result<District<C, B>>() != null);
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}