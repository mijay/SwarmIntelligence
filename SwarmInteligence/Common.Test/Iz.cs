using System;
using NUnit.Framework.Constraints;

namespace CommonTest
{
    public static class Iz
    {
        public static IResolveConstraint Any
        {
            get { return new MatchesContraint<object>(_ => true); }
        }

        public static IResolveConstraint Match<T>(Predicate<T> predicate)
        {
            return new MatchesContraint<T>(predicate);
        }

        #region Nested type: MatchesContraint

        private class MatchesContraint<T>: IResolveConstraint
        {
            private readonly Predicate<T> predicate;

            public MatchesContraint(Predicate<T> predicate)
            {
                this.predicate = predicate;
            }

            #region Implementation of IResolveConstraint

            public Constraint Resolve()
            {
                return new ConstraintExpression().Matches(predicate);
            }

            #endregion
        }

        #endregion
    }
}