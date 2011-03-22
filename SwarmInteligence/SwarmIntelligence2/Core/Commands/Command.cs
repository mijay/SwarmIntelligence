using System.Diagnostics.Contracts;

namespace SwarmIntelligence2.Core.Commands
{
    [ContractClass(typeof(ContractCommand<,,>))]
    public abstract class Command<C, B, E>
        where C: ICoordinate<C>
    {
        public abstract void Evaluate(EvaluationContext<C, B, E> context);
    }

    [ContractClassFor(typeof(Command<,,>))]
    internal abstract class ContractCommand<C, B, E>: Command<C, B, E>
        where C: ICoordinate<C>
    {
        public override void Evaluate(EvaluationContext<C, B, E> context)
        {
            Contract.Requires(context.Map != null &&
                              context.Ant != null &&
                              context.Cell != null &&
                              context.NodeDataLayer != null);
        }
    }
}