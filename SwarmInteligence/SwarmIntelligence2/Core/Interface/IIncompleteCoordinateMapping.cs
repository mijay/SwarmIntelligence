using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Interface
{
    [ContractClass(typeof(ContractIIncompleteCoordinateMapping<,>))]
    public interface IIncompleteCoordinateMapping<C, TData>: ICoordinateMapping<C, TData>
        where C: ICoordinate<C>
    {
        [Pure]
        bool HasData(C coord);

        void ClearData(C coord);
        IEnumerable<KeyValuePair<C, TData>> GetExistenData();
    }

    [ContractClassFor(typeof(IIncompleteCoordinateMapping<,>))]
    internal abstract class ContractIIncompleteCoordinateMapping<C, TData>: IIncompleteCoordinateMapping<C, TData>
        where C: ICoordinate<C>
    {
        #region Implementation of IIncompleteCoordinateMapping<C,TData>

        public bool HasData(C coord)
        {
            Contract.Requires(RangeValidator<C>.Instance.IsInRange(Range, coord));
            throw new NotImplementedException();
        }

        public void ClearData(C coord)
        {
            Contract.Requires(RangeValidator<C>.Instance.IsInRange(Range, coord));
            Contract.Requires(HasData(coord));
            Contract.Ensures(!HasData(coord));
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<C, TData>> GetExistenData()
        {
            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<KeyValuePair<C, TData>>>(),
                                             pair => HasData(pair.Key) && this[pair.Key].Equals(pair.Value)));
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ICoordinateMapping<C,out TData>

        public Range<C> Range
        {
            get { throw new NotImplementedException(); }
        }

        public TData this[C coord]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}