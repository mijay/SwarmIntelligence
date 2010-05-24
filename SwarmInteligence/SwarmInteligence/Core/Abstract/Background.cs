namespace SwarmInteligence
{
    public abstract class Background<C, B>
        where C: struct, ICoordinate<C>
    {
        public abstract B this[C index] { get; }
    }
}