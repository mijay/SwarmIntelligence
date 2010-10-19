using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Utils;

namespace SwarmInteligence
{
    /// <summary>
    /// Interface for representing coordinates on the <see cref="Map{C,B}"/>.
    /// </summary>
    /// <typeparam name="C"> Type of structure that implements the <see cref="ICoordinate{C}"/> interface. </typeparam>
    /// <remarks>
    /// For performance purpose all instances of <see cref="ICoordinate{C}"/> should be structures.
    /// Also everywhere where <see cref="ICoordinate{C}"/> are used they should be made a generic parameter, like this:
    /// <code>public interface IExample&lt;C&gt; where C: struct, ICoordinate&lt;C&gt; {...}</code>
    /// 
    /// In implementation <typeparamref name="C"/> should be the type of that implementation. For example:
    /// <code>public struct Coordinate: ICoordinate&lt;Coordinate&gt; {...}</code>
    /// This construction is useful for determine that all methods of the interface
    /// in implementation can work only with the objects of the same implementation. And is useful for performance.
    /// </remarks>
    [ContractClass(typeof(CoordinateContract<>))]
    public interface ICoordinate<C>: ICloneable, IEquatable<C>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// This getter is redundant and returns the structure itself.
        /// But it is useful for defining contracts.
        /// </summary>
        /// <example>
        /// This property implementation usual should be like this:
        /// <code>Coordinate2D Cast { get { return this; } }</code>
        /// </example>
        [Pure]
        [Obsolete("For contracts only")]
        C Cast { get; }

        /// <summary>
        /// Enumerate over all <see cref="ICoordinate{C}"/> which are in the cube
        /// defined by it's <paramref name="upperBound"/> and current <see cref="ICoordinate{C}"/> as the lowerBound.
        /// </summary>
        [Pure]
        IEnumerable<C> Range(C upperBound);

        /// <summary>
        /// Test if the current <see cref="ICoordinate{C}"/> is in <see cref="Range"/>
        /// between <paramref name="upperBound"/> and <paramref name="lowerBound"/>
        /// </summary>
        [Pure]
        bool IsInRange(C lowerBound, C upperBound);
    }

    [ContractClassFor(typeof(ICoordinate<>))]
    internal sealed class CoordinateContract<C>: ICoordinate<C>
        where C: struct, ICoordinate<C>
    {
        #region Implementation of ICloneable

        public object Clone()
        {
            Contract.Ensures(Contract.Result<object>().GetType() == GetType());
            Contract.Ensures(Equals(Contract.Result<object>() as ICoordinate<C>));
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IEquatable<ICoordinate<C>>

        public bool Equals(C other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ICoordinate<C>

        public IEnumerable<C> Range(C upperBound)
        {
            Contract.Ensures(Contract.Result<IEnumerable<C>>() != null);
            // check that this (as lowerBound) is the first point in range
            Contract.Ensures(Contract.Result<IEnumerable<C>>().First().Equals(Cast));
            // check that upperBound is the last point in range
            Contract.Ensures(Contract.Result<IEnumerable<C>>().Last().Equals(upperBound));
            // check that for all element in range IsInRange is true
            Contract.ForAll(Contract.Result<IEnumerable<C>>(), c => c.IsInRange(Cast, upperBound));
            // check that all point in suburb are distinct
            Contract.Ensures(Contract.Result<IEnumerable<C>>().AreDistinct());
            // checks that if lowerBound == upperBound then only one element returned and this element == this
            Contract.Ensures(!Equals(upperBound) || Contract.Result<IEnumerable<C>>().Single().Equals(Cast));
            throw new NotImplementedException();
        }

        public bool IsInRange(C lowerBound, C upperBound)
        {
            // check that IsInRange == lowerBound.Range(upperBound).Contains(this)
            Contract.Ensures(Contract.Result<bool>().Equals(lowerBound.Range(upperBound).Contains(Cast)));
            throw new NotImplementedException();
        }

        public C Cast
        {
            get
            {
                Contract.Ensures(Equals(Contract.Result<C>()));
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}