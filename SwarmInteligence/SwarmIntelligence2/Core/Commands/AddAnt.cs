using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.Core.Commands
{
    public class AddAnt<C, B>: Command<C, B>
        where C: struct, ICoordinate<C>
    {
        public AddAnt(C targetPoint, Ant<C, B> ant)
        {
            TargetPoint = targetPoint;
            Ant = ant;
        }

        public C TargetPoint { get; set; }
        public Ant<C, B> Ant { get; set; }

        public override void EvaluateWith(CommandEvaluator<C, B> visitor)
        {
            visitor.Evaluate(this);
        }
    }
}