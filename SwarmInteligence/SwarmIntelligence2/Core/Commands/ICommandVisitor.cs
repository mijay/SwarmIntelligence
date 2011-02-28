using SwarmIntelligence2.Core.Commands.Implementation;
using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands
{
    public interface ICommandVisitor<C, B>
        where C: ICoordinate<C>
    {
        void Evaluate(AddAnt<C, B> command);
        void Evaluate(SelfRemove<C, B> command);
        void Evaluate(MoveTo<C, B> command);
    }
}