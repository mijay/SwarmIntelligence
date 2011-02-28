using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands.Implementation
{
    public class MoveTo<C, B>: Command<C, B>
        where C: ICoordinate<C>
    {
        public MoveTo(C targetPoint)
        {
            TargetPoint = targetPoint;
        }

        public C TargetPoint { get; set; }

        public override void Visit(ICommandVisitor<C, B> visitor)
        {
            visitor.Evaluate(this);
        }
    }
}