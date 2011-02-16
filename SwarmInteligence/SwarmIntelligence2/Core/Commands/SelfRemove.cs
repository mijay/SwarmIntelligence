﻿using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2.Core.Commands
{
    public class SelfRemove<C, B>: Command<C, B>
        where C: struct, ICoordinate<C>
    {
        public override void EvaluateWith(CommandEvaluator<C, B> visitor)
        {
            visitor.Evaluate(this);
        }
    }
}