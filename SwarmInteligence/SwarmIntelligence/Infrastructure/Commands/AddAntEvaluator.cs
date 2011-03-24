using SwarmIntelligence.Core;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Commands
{
    public class AddAntEvaluator<C, B, E>: ITypedCommandDispatcher<AddAnt<C, B, E>>
        where C: ICoordinate<C>
    {
        #region Implementation of ITypedCommandDispatcher<in AddAnt<C,B,E>>

        public void Dispatch(AddAnt<C, B, E> command)
        {
            command.Context.World.Map[command.TargetPoint].Add(command.Ant);
        }

        #endregion
    }
}