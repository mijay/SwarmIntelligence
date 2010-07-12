using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SwarmInteligence
{
    [ContractClass(typeof(AirContract<,>))]
    public abstract class Air<C, B>: ICommunicative<C, B>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// Receive all messages which were sent in previous <see cref="TurnStage.Turn"/> and weren't mark with tag.
        /// </summary>
        [Pure]
        public abstract IEnumerable<IMessage> Messages { get; }

        /// <summary>
        /// Receive all messages associated with <paramref name="tag"/> which were sent in previous <see cref="TurnStage.Turn"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> is null, empty or whitespace.</exception>
        [Pure]
        public abstract IEnumerable<IMessage> this[string tag] { get; }

        #region IComponent<C,B> Members

        public District<C, B> District { get; set; }

        #endregion

        /// <summary>
        /// Send broadcast <paramref name="message"/> and mark it with the <paramref name="tags"/> (empty if none).
        /// This method can be used only in <see cref="TurnStage.Turn"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Any tag is null, empty or whitespace.</exception>
        public void SendMessage(IMessage message, params string[] tags)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(tags == null || tags.All(x => !String.IsNullOrWhiteSpace(x)));
            Contract.Requires<InvalidOperationException>(District.Stage == TurnStage.Turn);
            SendMessageWithTags(message, tags ?? new string[0]);
        }

        /// <summary>
        /// Add a listener which will be invoked in <see cref="TurnStage.ApplyTurn"/> and which can modify
        /// a list of <see cref="IMessage"/>s marked with some <paramref name="tag"/> which were sent in previous <see cref="TurnStage.Turn"/>
        /// (use <see cref="string.Empty"/> for messages which haven't been marked).
        /// </summary>
        /// <remarks>
        /// All <paramref name="listener"/>s for one <paramref name="tag"/> are invoked one-by-one.
        /// All <paramref name="listener"/>s are invoked before listeners from <see cref="AddMessagesListener"/> are invoked.
        /// </remarks>
        public abstract void AddMessageListener(Action<IList<IMessage>> listener, string tag);

        /// <summary>
        /// Add a listener which will be invoked in <see cref="TurnStage.ApplyTurn"/> and which can modify
        /// all lists of <see cref="IMessage"/>s which were sent in previous <see cref="TurnStage.Turn"/>
        /// (String.Empty is a key for messages which haven't been marked).
        /// </summary>
        /// <remarks>
        /// All <paramref name="listener"/>s are invoked one-by-one.
        /// All <paramref name="listener"/>s are invoked after listeners from <see cref="AddMessageListener"/> were invoked.
        /// </remarks>
        public abstract void AddMessagesListener(Action<IDictionary<string, IList<IMessage>>> listener);

        /// <inheritdoc/>
        /// <remarks>
        /// This message is broadcast and it is not marked with any tags.
        /// </remarks>
        void ICommunicative<C,B>.SendMessage(IMessage message)
        {
            SendMessage(message, new string[0]);
        }

        /// <summary>
        /// This is a background method for <see cref="ICommunicative{C,B}.SendMessage(SwarmInteligence.IMessage)"/> and <see cref="SendMessage(SwarmInteligence.IMessage,string[])"/>.
        /// </summary>
        /// <param name="message"><see cref="IMessage"/> to send broadcast.</param>
        /// <param name="tags">Tags to mark this message with (can be empty).</param>
        protected abstract void SendMessageWithTags(IMessage message, string[] tags);
    }

    [ContractClassFor(typeof(Air<,>))]
    internal class AirContract<C, B>: Air<C, B>
        where C: struct, ICoordinate<C>
    {
        #region Overrides of Air<C,B>

        public override IEnumerable<IMessage> Messages
        {
            get { throw new NotImplementedException(); }
        }

        public override IEnumerable<IMessage> this[string tag]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(tag));
                throw new NotImplementedException();
            }
        }

        public override void AddMessageListener(Action<IList<IMessage>> listener, string tag)
        {
            Contract.Requires<ArgumentNullException>(listener != null && tag != null);
            throw new NotImplementedException();
        }

        public override void AddMessagesListener(Action<IDictionary<string, IList<IMessage>>> listener)
        {
            Contract.Requires<ArgumentNullException>(listener != null);
            throw new NotImplementedException();
        }

        protected override void SendMessageWithTags(IMessage message, string[] tags)
        {
            Contract.Requires<ArgumentNullException>(message != null);
            Contract.Requires<ArgumentNullException>(tags.All(x => !String.IsNullOrWhiteSpace(x)));
            Contract.Requires<InvalidOperationException>(District.Stage == TurnStage.Turn);
            throw new NotImplementedException();
        }

        #endregion
    }
}