using SwarmIntelligence2.Core.Coordinates;
using Utils;

namespace SwarmIntelligence2.Core.Commands
{
    public class SelfRemove<C, B>: Command<C, B>
        where C: ICoordinate<C>
    {
        public override void Evaluate(EvaluationContext<C, B> context)
        {
            context.Cell.Remove(context.Ant);
            if (context.Cell.IsEmpty())
                context.Map.ClearData(context.Coordinate);
        }
    }
}