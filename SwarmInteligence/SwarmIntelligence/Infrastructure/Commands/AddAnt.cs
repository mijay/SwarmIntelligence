using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Commands
{
    public class AddAnt<C, B, E>: CommandImplementationBase<C, B, E>
        where C: ICoordinate<C>
    {
        public AddAnt(C targetPoint, Ant<C, B, E> ant)
        {
            TargetPoint = targetPoint;
            Ant = ant;
        }

        public C TargetPoint { get; set; }
        public Ant<C, B, E> Ant { get; set; }

        #region Overrides of Command<C,B,E>

        public override void Dispatch(ICommandDispatcher<C, B, E> dispatcher)
        {
            dispatcher.Dispatch(this);
        }

        #endregion
    }
}