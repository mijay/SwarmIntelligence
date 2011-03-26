using Common;
using Common.Collections;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Infrastructure.CommandsInfrastructure;

namespace SwarmIntelligence.Infrastructure.Commands
{
    public static class CommandEvaluationHelpers
    {
        public static void MoveAntFromCell<C, B, E>(CommandImplementationBase<C, B, E> command) where C: ICoordinate<C>
        {
            C previousCoordinate = command.Context.Coordinate;
            Cell<C, B, E> previousCell = command.Context.World.Map[previousCoordinate];
            previousCell.Remove(command.Context.Ant);
            if(previousCell.IsEmpty())
                command.Context.World.Map.Free(previousCoordinate);
        }
    }
}