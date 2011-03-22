﻿using SwarmIntelligence2.Core.Creatures;

namespace SwarmIntelligence2.Core.Commands
{
    public class AddAnt<C, B, E>: Command<C, B, E>
        where C: ICoordinate<C>
    {
        public AddAnt(C targetPoint, Ant<C, B, E> ant)
        {
            TargetPoint = targetPoint;
            Ant = ant;
        }

        public C TargetPoint { get; set; }
        public Ant<C, B, E> Ant { get; set; }

        public override void Evaluate(EvaluationContext<C, B, E> context)
        {
            context.Map[TargetPoint].Add(Ant);
        }
    }
}