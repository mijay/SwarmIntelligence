using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SwarmInteligence
{
    public sealed class Command
    {
        private readonly List<Action> pendingActions = new List<Action>();

        [ContractInvariantMethod]
        private void CommandInvariant()
        {
            Contract.Invariant(pendingActions != null);
        }

        public void Add(Action action)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            pendingActions.Add(action);
        }

        public void Add<T>(Action<T> action, T param)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            //создание замыканий не страшно, тк в любом случае пришлось бы ссоздавать объект
            pendingActions.Add(() => action.Invoke(param));
        }

        public void Add<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            pendingActions.Add(() => action.Invoke(param1, param2));
        }
    }
}