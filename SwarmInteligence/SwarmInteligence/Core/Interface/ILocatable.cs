using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    /// <summary>
    /// Represents all objects that can be placed on the <see cref="Map{C,B}"/>
    /// </summary>
    [ContractClass(typeof(LocatableContract<,>))]
    public interface ILocatable<C, B>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// <see cref="District{C,B}"/> where the object is stored.
        /// </summary>
        [Pure]
        District<C, B> District { get; }

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
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(District != null);
            Contract.Invariant(Coordinate.IsInRange(District.Bounds.Item1, District.Bounds.Item2),
                               "ILocatable belongs to the wrong District");
        }

        #region Implementation of ILocatable<C,B>

        public District<C, B> District
        {
            get
            {
                Contract.Ensures(Contract.Result<District<C, B>>() != null);
                throw new NotImplementedException();
            }
        }

        public C Coordinate
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

    public static class LocatableExtend
    {
        /// <summary>
        /// Gets the <see cref="Map{C,B}"/> to which this objects belongs.
        /// </summary>
        public static Map<C, B> Map<C, B>(this ILocatable<C, B> locatable)
            where C: struct, ICoordinate<C>
        {
            Contract.Requires<ArgumentNullException>(locatable != null);
            return locatable.District.Map;
        }

        /// <summary>
        /// Gets the <see cref="Air"/> which is used for communication in <see cref="District{C,B}"/>
        /// in which object is stored.
        /// </summary>
        public static Air Air<C, B>(this ILocatable<C, B> locatable)
            where C: struct, ICoordinate<C>
        {
            Contract.Requires<ArgumentNullException>(locatable != null);
            return locatable.District.Air;
        }
    }
}