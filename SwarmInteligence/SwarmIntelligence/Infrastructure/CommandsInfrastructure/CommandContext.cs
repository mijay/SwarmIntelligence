using System.Threading;
using Common;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Creatures;
using SwarmIntelligence.Core.Space;

namespace SwarmIntelligence.Infrastructure.CommandsInfrastructure
{
    public class CommandContext<C, B, E>: ICloneable<CommandContext<C, B, E>>
        where C: ICoordinate<C>
    {
        public static ThreadLocal<CommandContext<C, B, E>> CurrentContext;

        public World<C, B, E> World { get; set; }
        public Ant<C, B, E> Ant { get; set; }
        public C Coordinate { get; set; }

        #region Implementation of ICloneable

        public CommandContext<C, B, E> Clone()
        {
            return (CommandContext<C, B, E>) MemberwiseClone();
        }

        #endregion
    }
}