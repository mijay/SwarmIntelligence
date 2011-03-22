using System;

namespace SwarmInteligence
{
    /// <summary>
    /// <see cref="IMessage"/> objects are used as messages in communications via <see cref="Air{C,B}"/> and <see cref="ICommunicative{C,B}"/>.
    /// <see cref="IMessage"/> can contain one or few values. For single-value <see cref="IMessage"/> using of the <see cref="Content{T}"/> method 
    /// for obtaining data is recommended. Otherwise - <see cref="Get{T}"/>.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Get the data associated with the <paramref name="index"/>.
        /// </summary>
        /// <exception cref="InvalidCastException"> If the <typeparamref name="T"/> is incorrect. </exception>
        /// <exception cref="IndexOutOfRangeException"> There is no data associated with <paramref name="index"/>. </exception>
        T Get<T>(string index);

        /// <summary>
        /// Gets the typed content of the message.
        /// </summary>
        /// <exception cref="InvalidCastException"> If the <typeparamref name="T"/> is incorrect. </exception>
        T Content<T>();
    }
}