using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUtil
{
    public class ExtLog
    {
        public static ILog Instance;

        public static void RegisterExtLog(ILog log)
        {
            Instance = log;
        }

        public static void T(string format, params object[] args)
        {
            if (Instance != null)
                Instance.T(format, args);
        }

        public static void D(string format, params object[] args)
        {
            if (Instance != null)
                Instance.D(format, args);
        }

        public static void I(string format, params object[] args)
        {
            if (Instance != null)
                Instance.I(format, args);
        }

        public static void W(string format, params object[] args)
        {
            if (Instance != null)
                Instance.W(format, args);
        }

        public static void E(string format, params object[] args)
        {
            if (Instance != null)
                Instance.E(format, args);
        }
    }
}
