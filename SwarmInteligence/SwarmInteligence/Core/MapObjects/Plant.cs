namespace SwarmInteligence
{
    /// <summary>
    /// Abstract class for representing plant-like object on the <see cref="Map{C,B}"/>.
    /// Such objects can handle only <see cref="TurnStage.AfterTurn"/> stage, cannot move,
    /// cannot access anything outside it's <see cref="Cell{C,B}"/>.
    /// </summary>
    public abstract class Plant<C, B>: Stone<C, B>
        where C: struct, ICoordinate<C>
    {
        protected Plant(District<C, B> district): base(district) {}

        /// <summary>
        /// Kills current <see cref="Plant{C,B}"/> and removes it from <see cref="Map{C,B}"/>.
        /// All actions which were already stored in the <see cref="Command"/> will be invoked,
        /// but no actions could be stored in it anymore.
        /// Any stage handler like <see cref="OnAfterTurn"/> would not be invoked.
        /// </summary>
        protected void Die()
        {
            command.Add(Cell.RemoveNow, this);
            command.Close();
        }

        /// <summary>
        /// This method is invoked on the <see cref="TurnStage.AfterTurn"/> stage.
        /// </summary>
        protected abstract void OnAfterTurn();

        //todo: написать обработчик ApplyAfterTurn если его нужно таки писать тут.
    }
}