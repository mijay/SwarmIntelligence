using System;
using System.Collections.Generic;

namespace SwarmInteligence
{
    /// <summary>
    /// Interface for representing coordinates on the <see cref="Map{C,B}"/>.
    /// </summary>
    /// <typeparam name="C"> Type of structure that implements the <see cref="ICoordinate{C}"/> interface. </typeparam>
    /// <remarks>
    /// For performance purpose all instances of <see cref="ICoordinate{C}"/> should be structures.
    /// Also everywhere where <see cref="ICoordinate{C}"/> are used they should be made a generic parameter, like this:
    /// <example>public interface IExample&lt;C&gt; where C: struct, ICoordinate&lt;C&gt; {...}</example>
    /// 
    /// In implementation <typeparamref name="C"/> should be the type of that implementation. For example:
    /// <example>public struct Coordinate: ICoordinate&lt;Coordinate&gt; {...}</example>
    /// This construction is useful for determine that all methods of the interface
    /// in implementation can work only with the objects of the same implementation. And is useful for performance.
    /// </remarks>
    public interface ICoordinate<C>: ICloneable, IEquatable<ICoordinate<C>>
        where C: struct, ICoordinate<C>
    {
        /// <summary> Find distance between two points specified by coordinates. </summary>
        //TODO: не факт что это нужно. Не факт, что будет всегда работать (напр: как определить на графе?)
        long Distance<T>(T t);

        /// <summary>
        /// Enumerate over all suburb of the point defined by the current <see cref="ICoordinate{C}"/>
        /// of the radius defined by <paramref name="radius"/>.
        /// </summary>
        //TODO: опять непонятно что с графами. Ну не заносить же ссылку на граф в каждую координату. Хотя возможно в этом есть смысл.
        IEnumerator<C> Suburb(long radius);
    }
}
