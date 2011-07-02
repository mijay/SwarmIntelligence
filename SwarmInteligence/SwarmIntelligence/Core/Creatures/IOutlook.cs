using SwarmIntelligence.Core.PlayingField;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Creatures
{
    public interface IOutlook<C,B,E>
        where C: ICoordinate<C>
    {
        World<C, B, E> World { get; }
        C Coordinate { get; }
        Cell<C, B, E> Cell { get; }
        Ant<C, B, E> Me { get; }
    }
}