using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TinyJUtil
{
    /// <summary>
    /// the Process Utility of JUtil
    /// </summary>
    public static class Process
    {
        /// <summary>
        /// run specified program 
        /// </summary>
        public static void Run(string fileName)
        {
            ProcessStartInfo pi = new ProcessStartInfo(fileName);
            System.Diagnostics.Process.Start(pi);
        }

        /// <summary>
        /// run specified program with arguments
        /// </summary>
        public static void Run(string fileName, string arguments)
        {
            ProcessStartInfo pi = new ProcessStartInfo(fileName, arguments);
            System.Diagnostics.Process.Start(pi);
        }

        /// <summary>
        /// Pauses execution until the requested window exists or timeout happen.
        /// </summary>
        /// <returns>
        /// Returns false if timeout occurred.
        /// </returns>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <param name="timeout">Timeout in seconds</param>
        public static bool WaitExists(string lpClassName, string lpWindowName, TimeSpan timeout)
        {
            if (!Exists(lpClassName, lpWindowName))
            {
                if (timeout <= TimeSpan.Zero)
                    return false;

                System.Threading.Thread.Sleep(Tick);

                timeout -= Tick;

                return WaitExists(lpClassName, lpWindowName, timeout);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Pauses execution until the requested window does not exist or timeout happen.
        /// </summary>
        /// <returns>
        /// Returns false if timeout occurred.
        /// </returns>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <param name="timeout">Timeout in seconds</param>
        public static bool WaitClose(string lpClassName, string lpWindowName, TimeSpan timeout)
        {
            if (Exists(lpClassName, lpWindowName))
            {
                if (timeout <= TimeSpan.Zero)
                    return false;

                System.Threading.Thread.Sleep(Tick);

                timeout -= Tick;

                return WaitClose(lpClassName, lpWindowName, timeout);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Pauses execution until the requested window exist.
        /// </summary>
        public static void WaitExists(string lpClassName, string lpWindowName)
        {
            if (!Exists(lpClassName, lpWindowName))
            {
                System.Threading.Thread.Sleep(Tick);

                WaitExists(lpClassName, lpWindowName);
            }
        }

        /// <summary>
        /// Pauses execution until the requested window does not exist.
        /// </summary>
        public static void WaitClose(string lpClassName, string lpWindowName)
        {
            if (Exists(lpClassName, lpWindowName))
            {
                System.Threading.Thread.Sleep(Tick);

                WaitClose(lpClassName, lpWindowName);
            }
        }

        /// <summary>
        /// determine specified window exists
        /// </summary>
        public static bool Exists(string lpClassName, string lpWindowName)
        {
            //
            // http://www.dotblogs.com.tw/chou/archive/2009/04/26/8180.aspx
            //

            IntPtr hwd = default(IntPtr);
            
            hwd = FindWindow(lpClassName, lpWindowName);

            return !hwd.Equals(IntPtr.Zero);
        }

        [DllImport("user32", EntryPoint = "FindWindowA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private static TimeSpan Tick = new TimeSpan(0, 0, 0, 0, 50 /* milliseconds */);


    } // end of Process
}
