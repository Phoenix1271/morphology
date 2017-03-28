using System.Threading;

namespace Morphology.Test.Support
{
    internal static class Some
    {
        #region Private Fields

        private static int _counter;

        #endregion

        #region Public Methods

        public static decimal Decimal()
        {
            return Int() + 0.123m;
        }

        public static int Int()
        {
            return Interlocked.Increment(ref _counter);
        }

        public static string String(string tag = null)
        {
            return (tag ?? "") + "__" + Int();
        }

        #endregion
    }
}
