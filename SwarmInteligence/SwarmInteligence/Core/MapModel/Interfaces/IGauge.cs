using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Utils;

namespace SwarmInteligence
{
    /// <summary>
    /// Instance of this interface is used for operation on <see cref="ICoordinate{C}"/>
    /// which depends on the current <see cref="Map{C,B}"/>'s topology.
    /// </summary>
    /// <typeparam name="C"> <see cref="ICoordinate{C}"/> for which this <see cref="IGauge{C}"/> can be used. </typeparam>
    //todo: как бы оптимизировать операции с ним? Выносить его в генерик-параметр неохото >< Пока делать делегатную обертку
    [ContractClass(typeof(GaugeContract<>))]
    public interface IGauge<C>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// Enumerate over all point (in local coordinates) from suburb of the <paramref name="center"/> with the <paramref name="radius"/>.
        /// </summary>
        [Pure]
        IEnumerable<C> Suburb(C center, int radius);

        /// <summary>
        /// Convert coordinates of the <paramref name="point"/> from local (in some <see cref="Suburb"/>) to global one.
        /// <paramref name="center"/> and <paramref name="radius"/> defines that <see cref="Suburb"/>.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="point"/> is outside of the <see cref="Suburb"/>
        /// of <paramref name="center"/> with <paramref name="radius"/>.
        /// </exception>
        [Pure]
        C ToGlobal(C point, C center, int radius);
    }

    [ContractClassFor(typeof(IGauge<>))]
    public class GaugeContract<C>: IGauge<C>
        where C: struct, ICoordinate<C>
    {
        #region Implementation of IGauge<C>

        public IEnumerable<C> Suburb(C center, int radius)
        {
            Contract.Requires<ArgumentException>(radius >= 0);
            // check that zero-point is inside the suburb
            Contract.Ensures(Contract.Result<IEnumerable<C>>().Any(c => c.Equals(default(C))));
            // check that all point in suburb are distinct
            Contract.Ensures(Contract.Result<IEnumerable<C>>().GroupBy(c => c).All(g => g.Count() == 1));
            // check that all point in suburb can be converted back to global coordinates
            Contract.Ensures(
                AssertHelper.DoNotThrow(() => Contract.Result<IEnumerable<C>>().Select(c => ToGlobal(c, center, radius))));
            throw new NotImplementedException();
        }

        public C ToGlobal(C point, C center, int radius)
        {
            Contract.Requires<IndexOutOfRangeException>(Suburb(center, radius).Contains(point));
            throw new NotImplementedException();
        }

        #endregion
    }
}