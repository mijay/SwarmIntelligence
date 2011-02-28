using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands.Implementation
{
    public class AddAnt<C, B>: Command<C, B>
        where C: ICoordinate<C>
    {
        public AddAnt(C targetPoint, Ant<C, B> ant)
        {
            TargetPoint = targetPoint;
            Ant = ant;
        }

        public C TargetPoint { get; set; }
        public Ant<C, B> Ant { get; set; }

        public override void Visit(ICommandVisitor<C, B> visitor)
        {
            visitor.Evaluate(this);
        }
    }
}