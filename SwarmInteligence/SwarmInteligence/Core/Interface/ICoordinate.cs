using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SwarmInteligence
{
    /// <summary>
    /// Interface for representing coordinates on the <see cref="Map{C,B}"/>.
    /// </summary>
    /// <typeparam name="C"> Type of structure that implements the <see cref="ICoordinate{C}"/> interface. </typeparam>
    /// <remarks>
    /// For performance purpose all instances of <see cref="ICoordinate{C}"/> should be structures.
    /// Also everywhere where <see cref="ICoordinate{C}"/> are used they should be made a generic parameter, like this:
    /// <example>public interface IExample&lt;C&gt; where C: struct, ICoordinate&lt;C&gt; {...}</example>
    /// 
    /// In implementation <typeparamref name="C"/> should be the type of that implementation. For example:
    /// <example>public struct Coordinate: ICoordinate&lt;Coordinate&gt; {...}</example>
    /// This construction is useful for determine that all methods of the interface
    /// in implementation can work only with the objects of the same implementation. And is useful for performance.
    /// </remarks>
    [ContractClass(typeof(CoordinateContract<>))]
    public interface ICoordinate<C>: ICloneable, IEquatable<ICoordinate<C>>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// Enumerate over all point from suburb of the point defined by the current <see cref="ICoordinate{C}"/>
        /// of the radius defined by <paramref name="radius"/>.
        /// </summary>
        //TODO: опять непонятно что с графами. Ну не заносить же ссылку на граф в каждую координату. Хотя возможно в этом есть смысл.
        //todo: подумай ка еще раз об этом методе и его взаимоотношении с Range
        [Pure]
        IEnumerable<C> Suburb(long radius);

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

        [Pure]
        public object Clone()
        {
            Contract.Ensures(Contract.Result<object>().GetType()==this.GetType());
            Contract.Ensures(this.Equals(Contract.Result<object>() as ICoordinate<C>));
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IEquatable<ICoordinate<C>>

        [Pure]
        public bool Equals(ICoordinate<C> other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ICoordinate<C>

        public IEnumerable<C> Suburb(long radius)
        {
            // check that this point is inside the suburb
            Contract.Ensures(Contract.Result<IEnumerable<C>>().Any(c => c.Equals(this)));
            // check that all point in suburb are distinct
            Contract.Ensures(Contract.Result<IEnumerable<C>>().GroupBy(c => c).All(g => g.Count() == 1));
            throw new NotImplementedException();
        }

        public IEnumerable<C> Range(C upperBound)
        {
            // check that this (as lowerBound) is the first point in range
            Contract.Ensures(Contract.Result<IEnumerable<C>>().First().Equals(this));
            // check that upperBound is the last point in range
            Contract.Ensures(Contract.Result<IEnumerable<C>>().Last().Equals(upperBound));
            // check that for all all element in range IsInRange is true
            //Contract.Ensures(Contract.Result<IEnumerable<C>>().All(c => c.IsInRange(this, upperBound)));
            throw new NotImplementedException();
        }

        public bool IsInRange(C lowerBound, C upperBound)
        {
            // check that IsInRange == lowerBound.Range(upperBound).Contains(this)
            Contract.Ensures(Contract.Result<bool>().Equals(lowerBound.Range(upperBound).Cast<ICoordinate<C>>().Contains(this)));
            throw new NotImplementedException();
        }

        #endregion
    }
}
