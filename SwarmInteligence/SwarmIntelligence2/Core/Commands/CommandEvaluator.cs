using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.Interface;
using Utils;

namespace SwarmIntelligence2.Core.Commands
{
    public class CommandEvaluator<C, B>
        where C: struct, ICoordinate<C>
    {
        public EvaluationContext<C, B> EvaluationContext { get; set; }

        #region Implementation of ICommandEvaluator

        public void Evaluate(AddAnt<C, B> command)
        {
            Contract.Requires<ArgumentNullException>(command != null);
            Contract.Requires<InvalidOperationException>(EvaluationContext.IsFullFilled());

            EvaluationContext.Map[command.TargetPoint].Add(command.Ant);
        }

        public void Evaluate(SelfRemove<C, B> command)
        {
            Contract.Requires<ArgumentNullException>(command != null);
            Contract.Requires<InvalidOperationException>(EvaluationContext.IsFullFilled());

            MoveAntFromCell();
        }

        public void Evaluate(MoveTo<C, B> command)
        {
            Contract.Requires<ArgumentNullException>(command != null);
            Contract.Requires<InvalidOperationException>(EvaluationContext.IsFullFilled());

            MoveAntFromCell();
            EvaluationContext.Map[command.TargetPoint].Add(EvaluationContext.Ant);
        }

        #endregion

        private void MoveAntFromCell()
        {
            EvaluationContext.Cell.Remove(EvaluationContext.Ant);
            if (EvaluationContext.Cell.IsEmpty())
                EvaluationContext.Map.ClearData(EvaluationContext.Coordinate);
        }
    }
}