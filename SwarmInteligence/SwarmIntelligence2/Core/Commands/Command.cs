using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands
{
    public abstract class Command<C, B>
        where C: ICoordinate<C>
    {
        public abstract void EvaluateWith(CommandEvaluator<C, B> visitor);
    }
}