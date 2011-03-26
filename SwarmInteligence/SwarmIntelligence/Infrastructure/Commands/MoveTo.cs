using System;
using SwarmIntelligence.Core;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Commands
{
    public class MoveTo<C, B, E>: CommandImplementationBase<C, B, E>
        where C: ICoordinate<C>
    {
        public MoveTo(C targetPoint)
        {
            TargetPoint = targetPoint;
        }

        public C TargetPoint { get; set; }

        #region Overrides of Command<C,B,E>

        public override void Dispatch(ICommandDispatcher<C, B, E> dispatcher)
        {
            dispatcher.Dispatch(this);
        }

        #endregion
    }
}