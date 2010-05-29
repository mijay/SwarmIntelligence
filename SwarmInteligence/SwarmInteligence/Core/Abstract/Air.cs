using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    [ContractClass(typeof(AirContract<,>))]
    public abstract class Air<C, B>
        where C: struct, ICoordinate<C>
    {
        [ContractPublicPropertyName("District")]
        private District<C, B> district;

        public District<C, B> District
        {
            [Pure]
            get
            {
                Contract.Requires<InvalidOperationException>(district != null, "District hasn't been initialized");
                Contract.Ensures(Contract.Result<District<C, B>>() != null);
                return district;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<InvalidOperationException>(district == null, "District was set already");
                district = value;
            }
        }

        [Pure]
        public abstract IEnumerable<IMessage> Messages { get; }

        [Pure]
        public abstract IEnumerable<IMessage> this[string tag] { get; }

        public abstract void SendMessage(IMessage message);

        public abstract void SendTaggedMessage(IMessage message, params string[] tags);
    }

    [ContractClassFor(typeof(Air<,>))]
    internal class AirContract<C, B>: Air<C, B>
        where C: struct, ICoordinate<C>
    {
        #region Overrides of Air<C,B>

        public override IEnumerable<IMessage> Messages
        {
            get
            {
                Contract.Requires<InvalidOperationException>(District.Stage == TurnStage.AfterTurn);
                throw new NotImplementedException();
            }
        }

        public override IEnumerable<IMessage> this[string tag]
        {
            get
            {
                Contract.Requires<InvalidOperationException>(District.Stage == TurnStage.AfterTurn);
                throw new NotImplementedException();
            }
        }

        public override void SendMessage(IMessage message)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<InvalidOperationException>(District.Stage == TurnStage.Turn);
            throw new NotImplementedException();
        }

        public override void SendTaggedMessage(IMessage message, params string[] tags)
        {
            Contract.Requires<ArgumentNullException>(message != null && tags != null && tags.Length > 0);
            Contract.Requires<InvalidOperationException>(District.Stage == TurnStage.Turn);
            throw new NotImplementedException();
        }

        #endregion
    }
}