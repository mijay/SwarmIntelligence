using System.Collections.Generic;

namespace SwarmInteligence.Core.Messages
{
    public class AbstractCommunicative<C, B>: AbstractComponent<C, B>, ICommunicative<C, B>
        where C: struct, ICoordinate<C>
    {
        public AbstractCommunicative(District<C, B> district): base(district)
        {
            district.OnApplyTurnStage += () => {
                                             receivedMessages = receivingMessages;
                                             receivingMessages = new List<IMessage>();
                                         };
        }

        /// <summary>
        /// <see cref="IMessage"/>s which were received during previous <see cref="TurnStage.Turn"/>
        /// and which are acceptable until end of the current <see cref="TurnStage.Turn"/>.
        /// </summary>
        private IList<IMessage> receivedMessages = new List<IMessage>();

        /// <summary>
        /// <see cref="IMessage"/>s which are receiving during current <see cref="TurnStage.Turn"/> which is current stage
        /// </summary>
        private IList<IMessage> receivingMessages = new List<IMessage>();

        /// <inheritdoc/>
        public void SendMessage(IMessage message)
        {
            receivingMessages.Add(message);
        }

        /// <summary>
        /// Gets all <see cref="IMessage"/>s which were received during previous <see cref="TurnStage.Turn"/>.
        /// </summary>
        public IEnumerable<IMessage> ReceivedMessages
        {
            get { return receivedMessages; }
        }
    }
}