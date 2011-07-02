using Common.Collections;
using SwarmIntelligence.Core.PlayingField;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Core.Creatures
{
    public static class OutlookExtensions
    {
        public static void MoveTo<C, B, E>(this IOutlook<C, B, E> outlook, C to) where C: ICoordinate<C>
        {
            outlook.Remove();
            outlook.World.Map[to].Add(outlook.Me);
        }

        public static void Remove<C, B, E>(this IOutlook<C, B, E> outlook) where C: ICoordinate<C>
        {
            C previousCoordinate = outlook.Coordinate;
            Cell<C, B, E> previousCell = outlook.World.Map[previousCoordinate];
            previousCell.Remove(outlook.Me);
            if(previousCell.IsEmpty())
                outlook.World.Map.Free(previousCoordinate);
        }
    }
}