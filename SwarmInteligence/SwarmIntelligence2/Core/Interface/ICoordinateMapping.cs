using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Interface
{
    /// <summary>
    /// Specify object that associates some value (of type <typeparamref name="TData"/>)
    /// for each point specified by coordinate (of type <typeparamref name="C"/>) limited to <see cref="Range"/>.
    /// </summary>
    /// <typeparam name="C">Type of the coordinates.</typeparam>
    /// <typeparam name="TData">Type of the stored data.</typeparam>
    [ContractClass(typeof(ContractICoordinateMapping<,>))]
    public interface ICoordinateMapping<C, out TData>
        where C: ICoordinate<C>
    {
        [Pure]
        Range<C> Range { get; }

        TData this[C coord] { get; }
    }

    [ContractClassFor(typeof(ICoordinateMapping<,>))]
    internal abstract class ContractICoordinateMapping<C, TData>: ICoordinateMapping<C, TData>
        where C: ICoordinate<C>
    {
        #region Implementation of ICoordinateMapping<C,out TData>

        public Range<C> Range
        {
            get { throw new NotImplementedException(); }
        }

        public TData this[C coord]
        {
            get
            {
                Contract.Requires(RangeValidator<C>.Instance.IsInRange(Range, coord));
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}