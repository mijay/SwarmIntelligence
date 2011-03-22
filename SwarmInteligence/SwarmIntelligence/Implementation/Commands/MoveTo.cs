using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Commands;
using Utils;

namespace SwarmIntelligence.Implementation.Commands
{
    public class MoveTo<C, B, E>: Command<C, B, E>
        where C: ICoordinate<C>
    {
        public MoveTo(C targetPoint)
        {
            TargetPoint = targetPoint;
        }

        public C TargetPoint { get; set; }

        public override void Evaluate(EvaluationContext<C, B, E> context)
        {
            context.Cell.Remove(context.Ant);
            if(context.Cell.IsEmpty())
                context.Map.Free(context.Coordinate);
            context.Map[TargetPoint].Add(context.Ant);
        }
    }
}