using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace SwarmInteligence
{
    public sealed class Command
    {
        private readonly List<Action> pendingActions = new List<Action>();
        public bool Closed { get; private set; }

        [ContractInvariantMethod]
        private void CommandInvariant()
        {
            Contract.Invariant(pendingActions != null);
        }

        public void Add(Action action)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            Contract.Requires<InvalidOperationException>(!Closed, "Object is closed already");
            pendingActions.Add(action);
        }

        public void Add<T>(Action<T> action, T param)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            Contract.Requires<InvalidOperationException>(!Closed, "Object is closed already");
            //создание замыканий не страшно, тк в любом случае пришлось бы создавать объект
            pendingActions.Add(() => action.Invoke(param));
        }

        public void Add<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            Contract.Requires<InvalidOperationException>(!Closed, "Object is closed already");
            pendingActions.Add(() => action.Invoke(param1, param2));
        }

        public void Close()
        {
            Closed = true;
        }

        internal void Reset()
        {
            pendingActions.Clear();
        }

        internal void Run()
        {
            ParallelLoopResult result = Parallel.ForEach(pendingActions, d => d());
            Contract.Assert(result.IsCompleted);
            pendingActions.Clear();
        }
    }
}