using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    /// <summary>
    /// Represent all objects that are associated with some <see cref="District{C,B}"/>
    /// </summary>
    [ContractClass(typeof(ComponentContract<,>))]
    public interface IComponent<C, B>
        where C: struct, ICoordinate<C>
    {
        /// <summary>
        /// <see cref="District{C,B}"/> where the object is stored.
        /// </summary>
        [Pure]
        District<C, B> District { get; }
    }

    [ContractClassFor(typeof(IComponent<,>))]
    internal class ComponentContract<C, B>: IComponent<C, B>
        where C: struct, ICoordinate<C>
    {
        #region Implementation of IComponent<C,B>

        public District<C, B> District
        {
            get
            {
                Contract.Ensures(Contract.Result<District<C, B>>() != null);
                throw new NotImplementedException();
            }
        }

        #endregion
    }

    public static class ComponentExtend
    {
        /// <summary>
        /// Gets the <see cref="Map{C,B}"/> to which this objects belongs.
        /// </summary>
        public static Map<C, B> Map<C, B>(this IComponent<C, B> component)
            where C: struct, ICoordinate<C>
        {
            Contract.Requires<ArgumentNullException>(component != null);
            return component.District.Map;
        }

        /// <summary>
        /// Gets the <see cref="SwarmInteligence.Air{C,B}"/> which is used for communication in <see cref="District{C,B}"/>
        /// in which object is stored.
        /// </summary>
        public static Air<C, B> Air<C, B>(this IComponent<C, B> component)
            where C: struct, ICoordinate<C>
        {
            Contract.Requires<ArgumentNullException>(component != null);
            return component.District.Air;
        }
    }
}