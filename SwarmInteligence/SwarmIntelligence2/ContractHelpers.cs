using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.Commands;
using SwarmIntelligence2.Core.Interface;

namespace SwarmIntelligence2
{
    internal static class ContractHelpers
    {
        [Pure]
        public static bool IsFullFilled<C, B>(this EvaluationContext<C, B> context) where C: struct, ICoordinate<C>
        {
            return context != null &&
                   context.Map != null &&
                   context.Ant != null &&
                   context.Cell != null;
        }
    }
}