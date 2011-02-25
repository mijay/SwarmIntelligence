﻿using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands
{
    public class MoveTo<C, B>: Command<C, B>
        where C: ICoordinate<C>
    {
        public MoveTo(C targetPoint)
        {
            TargetPoint = targetPoint;
        }

        public C TargetPoint { get; set; }

        public override void EvaluateWith(CommandEvaluator<C, B> visitor)
        {
            visitor.Evaluate(this);
        }
    }
}