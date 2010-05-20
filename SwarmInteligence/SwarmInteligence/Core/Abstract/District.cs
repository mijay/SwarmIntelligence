using System;
using System.Collections.Generic;

namespace SwarmInteligence
{
    public class District<C,B>
        where C:struct, ICoordinate<C>
    {
        public Map<C, B> Map { private set; get; }

        public Air Air { private set; get; }
    }
}
