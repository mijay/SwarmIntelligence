namespace SwarmInteligence
{
    /// <summary>
    /// Represent objects that can receive a <see cref="IMessage"/>.
    /// </summary>
    public interface ICommunicative
    {
        /// <summary>
        /// Send a <paramref name="message"/> to the object.
        /// </summary>
        void SendMessage(IMessage message);
    }
}
