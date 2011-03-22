using SwarmInteligence.Core.Messages;

namespace SwarmInteligence
{
    public class Animal<C,B>: AbstractCommunicative<C, B>
        where C: struct, ICoordinate<C> 
    {
        #region Implementation of ICommunicative<C,B>

        public Animal(Map<C, B> map): base(map) {}

        #endregion
    }
}