using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Utils;

namespace SwarmIntelligence.Infrastructure.Commands
{
    public class SelfRemove<C, B, E>: Command<C, B, E>
        where C: ICoordinate<C>
    {
        public override void Evaluate(EvaluationContext<C, B, E> context)
        {
            context.Cell.Remove(context.Ant);
            if(context.Cell.IsEmpty())
                context.Map.Free(context.Coordinate);
        }
    }
}