using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    /// <summary>
    /// Represent objects that can receive a <see cref="IMessage"/>.
    /// </summary>
    [ContractClass(typeof(CommunicativeContract<,>))]
    public interface ICommunicative<C, B>: ILocatable<C, B>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// Send a <paramref name="message"/> to the object.
        /// </summary>
        void SendMessage(IMessage message);
    }

    [ContractClassFor(typeof(ICommunicative<,>))]
    internal sealed class CommunicativeContract<C, B>: ICommunicative<C, B>
        where C: struct, ICoordinate<C>
    {
        #region Implementation of ILocatable<C,B>

        public District<C, B> District
        {
            get { throw new NotImplementedException(); }
        }

        public C Coordinate
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Implementation of ICommunicative<C,B>

        public void SendMessage(IMessage message)
        {
            Contract.Requires<InvalidOperationException>(District.Stage == TurnStage.Turn,
                                                         "Stone.SendMessage can be called only in Turn stage");
            Contract.Requires<ArgumentNullException>(message != null);
        }

        #endregion
    }
}