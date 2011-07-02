using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Commands
{
    public class MoveToEvaluator<C, B, E>: ITypedCommandDispatcher<MoveTo<C, B, E>, C, B, E>
        where C: ICoordinate<C>
    {
        #region Implementation of ITypedCommandDispatcher<in MoveTo<C,B,E>>

        public void Dispatch(MoveTo<C, B, E> command)
        {
            CommandEvaluationHelpers.MoveAntFromCell(command);
            command.Context.World.Map[command.TargetPoint].Add(command.Context.Ant);
        }

        #endregion
    }
}