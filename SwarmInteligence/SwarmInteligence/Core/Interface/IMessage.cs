using System;

namespace SwarmInteligence
{
    /// <summary>
    /// <see cref="IMessage"/> objects are used as messages in communications via <see cref="Air{C,B}"/> and <see cref="ICommunicative{C,B}"/>.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Content of the message.
        /// </summary>
        object Content { get; }

        /// <summary>
        /// Gets the typed content of the message.
        /// </summary>
        /// <exception cref="InvalidCastException"> If the <typeparamref name="T"/> is incorrect. </exception>
        T Value<T>();
    }
}