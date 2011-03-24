using System;

namespace SwarmIntelligence.Utils
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}