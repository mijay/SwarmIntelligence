using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Commands
{
    public class SelfRemove<C, B, E>: CommandImplementationBase<C, B, E>
        where C: ICoordinate<C>
    {
        #region Overrides of Command<C,B,E>

        public override void Dispatch(ICommandDispatcher<C, B, E> dispatcher)
        {
            dispatcher.Dispatch(this);
        }

        #endregion
    }
}