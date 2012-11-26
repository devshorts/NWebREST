using System.Threading;

namespace NetDuinoUtils.Utils
{
    public static class RunUtil
    {
        /// <summary>
        /// Empty while loop to keep the thread running
        /// </summary>
        public static void KeepRunning()
        {
            while (true)
            {
                Thread.Sleep(250);
            }
        }
    }
}
