using System.Threading;

namespace CreditCards.UITests
{
    internal static class DemoHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="secondsToPause"></param>
        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }
    }
}
