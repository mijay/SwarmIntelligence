using System;

namespace Utils
{
    public static class AssertHelper
    {
        public static Exception CatchException(Action action)
        {
            try {
                action();
            }
            catch(Exception e) {
                return e;
            }
            return null;
        }

        public static bool DoNotThrow(Action action)
        {
            return CatchException(action) == null;
        }
    }
}