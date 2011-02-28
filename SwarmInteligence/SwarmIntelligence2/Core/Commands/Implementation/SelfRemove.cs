using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands.Implementation
{
    public class SelfRemove<C, B>: Command<C, B>
        where C: ICoordinate<C>
    {
        public override void Visit(ICommandVisitor<C, B> visitor)
        {
            visitor.Evaluate(this);
        }
    }
}