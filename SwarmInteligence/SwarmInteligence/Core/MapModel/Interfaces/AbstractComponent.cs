namespace SwarmInteligence
{
    public class AbstractComponent<C, B> : IComponent<C, B>
        where C : struct, ICoordinate<C>
    {
        public AbstractComponent(District<C, B> district)
        {
            District = district;
        }

        public District<C, B> District { get; private set; }
    }
}