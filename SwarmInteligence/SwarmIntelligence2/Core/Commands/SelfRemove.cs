using Utils;

namespace SwarmIntelligence2.Core.Commands
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