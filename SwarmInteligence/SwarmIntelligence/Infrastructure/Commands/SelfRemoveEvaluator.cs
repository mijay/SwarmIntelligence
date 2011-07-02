using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Commands
{
    public class SelfRemoveEvaluator<C, B, E>: ITypedCommandDispatcher<SelfRemove<C, B, E>, C, B, E>
        where C: ICoordinate<C>
    {
        #region Implementation of ITypedCommandDispatcher<in SelfRemove<C,B,E>>

        public void Dispatch(SelfRemove<C, B, E> command)
        {
            CommandEvaluationHelpers.MoveAntFromCell(command);
        }

        #endregion
    }
}