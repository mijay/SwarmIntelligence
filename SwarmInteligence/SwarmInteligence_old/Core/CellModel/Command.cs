using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace SwarmInteligence
{
    /// <summary>
    /// Class for storing delegates on delayed actions. It implements the "command" pattern.
    /// </summary>
    public sealed class Command
    {
        private readonly List<Action> pendingActions = new List<Action>();

        /// <summary>
        /// Indicates if the <see cref="Command"/> was already <see cref="Close"/>d.
        /// </summary>
        public bool Closed { get; private set; }

        [ContractInvariantMethod]
        private void CommandInvariant()
        {
            Contract.Invariant(pendingActions != null);
        }

        /// <summary>
        /// Add new <paramref name="action"/> to the invocation list.
        /// </summary>
        public void Add(Action action)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            Contract.Requires<InvalidOperationException>(!Closed, "Command is closed already");
            pendingActions.Add(action);
        }

        /// <summary>
        /// Add new <paramref name="action"/> with parameter to the invocation list.
        /// </summary>
        public void Add<T>(Action<T> action, T param)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            Contract.Requires<InvalidOperationException>(!Closed, "Command is closed already");
            //создание замыканий не страшно, тк в любом случае пришлось бы создавать объект
            pendingActions.Add(() => action.Invoke(param));
        }

        /// <summary>
        /// Add new <paramref name="action"/> with two parameters to the invocation list.
        /// </summary>
        public void Add<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            Contract.Requires<InvalidOperationException>(!Closed, "Command is closed already");
            pendingActions.Add(() => action.Invoke(param1, param2));
        }

        /// <summary>
        /// Indicate that current <see cref="Command"/> is closed and no more actions can be added to it.
        /// </summary>
        public void Close()
        {
            Closed = true;
        }

        internal void Reset()
        {
            pendingActions.Clear();
            Closed = false;
        }

        internal void Run()
        {
            ParallelLoopResult result = Parallel.ForEach(pendingActions, d => d());
            Contract.Assert(result.IsCompleted);
            Reset();
        }
    }
}