using System;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    public class AbstractComponent<C, B> : IComponent<C, B>
        where C : struct, ICoordinate<C>
    {
        public AbstractComponent(Map<C, B> map)
        {
            Contract.Requires<ArgumentNullException>(map != null);
            Map = map;
        }

        public Map<C, B> Map { get; private set; }
    }
}