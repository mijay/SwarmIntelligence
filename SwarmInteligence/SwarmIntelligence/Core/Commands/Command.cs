namespace SwarmIntelligence.Core.Commands
{
    public abstract class Command<C, B, E>
        where C: ICoordinate<C>
    {
        public abstract void Evaluate(EvaluationContext<C, B, E> context);// todo снести и перевести на Dispatcher-ы
    }
}