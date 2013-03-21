using System;
using System.Diagnostics;
using JUtil.Path;
using System.Runtime.InteropServices;

namespace JUtil
{
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
        /// Pauses execution until the requested window exists.
        /// </summary>
        /// <returns>
        /// Returns false if timeout occurred.
        /// </returns>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <param name="timeout">Timeout in seconds</param>
        public static bool WaitExists(string lpClassName, string lpWindowName, int timeout)
        {
            if (!Exists(lpClassName, lpWindowName))
            {
                if (timeout == 0)
                    return false;

                System.Threading.Thread.Sleep(1000);

                return WaitExists(lpClassName, lpWindowName, timeout--);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Pauses execution until the requested window does not exist.
        /// </summary>
        /// <returns>
        /// Returns false if timeout occurred.
        /// </returns>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <param name="timeout">Timeout in seconds</param>
        public static bool WaitClose(string lpClassName, string lpWindowName, int timeout)
        {
            if (Exists(lpClassName, lpWindowName))
            {
                if (timeout == 0)
                    return false;

                System.Threading.Thread.Sleep(1000);

                return WaitClose(lpClassName, lpWindowName, timeout--);
            }
            else
            {
                return true;
            }
        }

        public static void WaitExists(string lpClassName, string lpWindowName)
        {
            if (!Exists(lpClassName, lpWindowName))
            {
                System.Threading.Thread.Sleep(1000);

                WaitExists(lpClassName, lpWindowName);
            }
        }

        public static void WaitClose(string lpClassName, string lpWindowName)
        {
            if (Exists(lpClassName, lpWindowName))
            {
                System.Threading.Thread.Sleep(1000);

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


    } // end of Process
}
