namespace SwarmInteligence
{
    /// <summary>
    /// Represents all objects that can be placed on the <see cref="Map{C,B}"/>
    /// </summary>
    public interface ILocatable<C, B>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// <see cref="District{C,B}"/> where the object is stored.
        /// </summary>
        District<C,B> District { get; }

        /// <summary>
        /// Coordinate of the object on the <see cref="Map{C,B}"/>
        /// </summary>
        C Coordinate { get; }
    }

    public static class LocatableExtend
    {
        /// <summary>
        /// Gets the <see cref="Map{C,B}"/> to which this objects belongs.
        /// </summary>
        public static Map<C,B> Map<C,B>(this ILocatable<C,B> locatable)
            where C: struct, ICoordinate<C>
        {
            return locatable.District.Map;
        }

        /// <summary>
        /// Gets the <see cref="Air"/> which is used for communication in <see cref="District{C,B}"/>
        /// in which object is stored.
        /// </summary>
        public static Air Air<C,B>(this ILocatable<C,B> locatable)
            where C: struct, ICoordinate<C>
        {
            return locatable.District.Air;
        }
    }
}