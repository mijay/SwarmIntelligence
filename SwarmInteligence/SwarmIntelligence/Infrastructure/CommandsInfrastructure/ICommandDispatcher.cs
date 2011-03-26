﻿using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public interface ICommandDispatcher<C, B, E>
        where C: ICoordinate<C>
    {
        void Dispatch<TCommand>(TCommand command) where TCommand: Command<C, B, E>;
    }
}