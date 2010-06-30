using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    /// <summary>
    /// Represents all objects that can be placed on the <see cref="Map{C,B}"/>
    /// </summary>
    [ContractClass(typeof(LocatableContract<,>))]
    public interface ILocatable<C, B>: IComponent<C, B>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// Coordinate of the object on the <see cref="Map{C,B}"/>
        /// </summary>
        [Pure]
        C Coordinate { get; }
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

        #endregion

        #region Implementation of IComponent<C,B>

        public District<C, B> District
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}