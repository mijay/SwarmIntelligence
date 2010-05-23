using System;
using System.Collections.Generic;
using System.Linq;

namespace SwarmInteligence
{
    public class Map<C, B>
        where C : struct, ICoordinate<C>
    {
        public C Size { get; private set; }
    }
}
