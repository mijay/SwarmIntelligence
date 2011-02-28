using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence2.Core.Coordinates;

namespace SwarmIntelligence2.Core.Commands
{
    [ContractClass(typeof(ContractCommand<,>))]
    public abstract class Command<C, B>
        where C: ICoordinate<C>
    {
        public abstract void Visit(ICommandVisitor<C, B> visitor);
    }

    [ContractClassFor(typeof(Command<,>))]
    internal abstract class ContractCommand<C, B>: Command<C, B>
        where C: ICoordinate<C>
    {
        public override void Visit(ICommandVisitor<C, B> visitor)
        {
            Contract.Requires<ArgumentNullException>(visitor != null);
        }
    }
}