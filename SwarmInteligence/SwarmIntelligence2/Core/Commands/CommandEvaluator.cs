using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.Core.Commands
{
    public class CommandEvaluator<C, B>
        where C: struct, ICoordinate<C>
    {
        public EvaluationContext<C, B> EvaluationContext { get; set; }

        #region Implementation of ICommandEvaluator

        public void Evaluate(AddAnt<C, B> command)
        {
            Contract.Requires<InvalidOperationException>(EvaluationContext.IsFullFilled());

            EvaluationContext.Map[command.TargetPoint].Add(command.Ant);
        }

        public void Evaluate(SelfRemove<C, B> command)
        {
            Contract.Requires<InvalidOperationException>(EvaluationContext.IsFullFilled());

            bool wasRemoved = EvaluationContext.Cell.Remove(EvaluationContext.Ant);

            Contract.Assert(wasRemoved);
        }

        public void Evaluate(MoveTo<C, B> command)
        {
            Contract.Requires<InvalidOperationException>(EvaluationContext.IsFullFilled());

            var wasMoved = EvaluationContext.Cell.Remove(EvaluationContext.Ant);
            EvaluationContext.Map[command.TargetPoint].Add(EvaluationContext.Ant);

            Contract.Assert(wasMoved);
        }

        #endregion
    }
}