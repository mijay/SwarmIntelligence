using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    [ContractClass(typeof(DistrictContract<,>))]
    public abstract class District<C, B>
        where C : struct, ICoordinate<C>
    {
        [Pure]
        protected District(Tuple<C, C> bounds)
        {
            Contract.Requires<ArgumentNullException>(bounds != null);
            Bounds = bounds;
        }

        //[Pure]
        //public abstract IList<Stone<C, B>> GetData(ICoordinate<C> coordinate);

        [Pure]
        public Tuple<C, C> Bounds { get; private set; }
    }

    [ContractClassFor(typeof(District<,>))]
    internal sealed class DistrictContract<C, B> : District<C, B>
        where C : struct, ICoordinate<C>
    {
        public DistrictContract(Tuple<C, C> bounds)
            : base(bounds) { }

        #region Overrides of District<C,B>

        //public override IList<Stone<C, B>> GetData(ICoordinate<C> coordinate)
        //{
        //    Contract.Requires<IndexOutOfRangeException>(coordinate.IsInRange(Bounds.Item1, Bounds.Item2));
        //    Contract.Ensures(Contract.Result<IList<Stone<C, B>>>() != null);
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}