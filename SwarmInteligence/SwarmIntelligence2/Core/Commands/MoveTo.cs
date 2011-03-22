using SwarmIntelligence2.Core.World;
using Utils;

namespace SwarmIntelligence2.Core.Commands
{
    public class MoveTo<C, B>: Command<C, B>
        where C: ICoordinate<C>
    {
        public MoveTo(C targetPoint)
        {
            TargetPoint = targetPoint;
        }

        public C TargetPoint { get; set; }

        public override void Evaluate(EvaluationContext<C, B> context)
        {
            context.Cell.Remove(context.Ant);
            if(context.Cell.IsEmpty())
                context.Map.Free(context.Coordinate);
            context.Map[TargetPoint].Add(context.Ant);
        }
    }
}