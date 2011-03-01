using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands
{
    [ContractClass(typeof(ContractCommand<,>))]
    public abstract class Command<C, B>
        where C: ICoordinate<C>
    {
        public abstract void Evaluate(EvaluationContext<C, B> context);
    }

    [ContractClassFor(typeof(Command<,>))]
    internal abstract class ContractCommand<C, B>: Command<C, B>
        where C: ICoordinate<C>
    {
        public override void Evaluate(EvaluationContext<C, B> context)
        {
            Contract.Requires(context.Map != null &&
                                                     context.Ant != null &&
                                                     context.Cell != null &&
                                                     context.Background != null);
        }
    }
}