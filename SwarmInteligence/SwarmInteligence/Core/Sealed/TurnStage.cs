namespace SwarmInteligence
{
    /// <summary>
    /// Represent the current stage of the turn.
    /// </summary>
    public enum TurnStage
    {
        /// <summary>
        /// At this stage <see cref="IMessage"/>s can be send, <see cref="Animal{C,B}"/>s can move,
        /// new <see cref="Stone{C,B}"/>s can be added. Both: the <see cref="Cell{C,B}"/> and
        /// the <see cref="Zone{C,B}"/> are accessible.
        /// </summary>
        Turn,
        /// <summary>
        /// At this stage <see cref="IMessage"/>s can be received, new <see cref="Stone{C,B}"/>s can be added.
        /// Only <see cref="Cell{C,B}"/> is accessible.
        /// </summary>
        AfterTurn,
        /// <summary>
        /// This stage is service only. Preparing to the <see cref="Turn"/>.
        /// </summary>
        BeforeTurn,
        /// <summary>
        /// This stage is service only. Applying <see cref="Command"/>s created at <see cref="Turn"/> stage.
        /// </summary>
        ApplyTurn,
        /// <summary>
        /// This stage is service only. Applying <see cref="Command"/>s created at <see cref="AfterTurn"/> stage.
        /// </summary>
        ApplyAfterTurn
    }

    public static class TurnStageExtend
    {
        public static bool IsService(this TurnStage stage)
        {
            return stage != TurnStage.Turn && stage != TurnStage.AfterTurn;
        }
    }
}