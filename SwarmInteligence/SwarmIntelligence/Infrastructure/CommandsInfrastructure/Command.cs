﻿using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public abstract class Command<C, B, E>
        where C: ICoordinate<C>
    {
        public abstract void Dispatch(ICommandDispatcher<C, B, E> dispatcher);
    }
}