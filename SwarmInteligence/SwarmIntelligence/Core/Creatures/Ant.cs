using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Creatures
{
    public abstract class Ant<C, B, E>
        where C: ICoordinate<C>
    {
        public abstract void ProcessTurn(IOutlook<C, B, E> outlook);
    }
}