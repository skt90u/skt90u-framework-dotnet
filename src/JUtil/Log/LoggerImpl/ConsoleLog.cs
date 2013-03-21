using System;

namespace JUtil
{
    internal sealed class ConsoleLog : ILog
    {
        #region ILog 成員

        enum LogState
        {
            T = 0,
            D = 1,
            I = 2,
            W = 3,
            E = 4
        }

        public void T(string format, params object[] args)
        {
            Output(LogState.T, format, args);
        }

        public void D(string format, params object[] args)
        {
            Output(LogState.D, format, args);
        }

        public void I(string format, params object[] args)
        {
            Output(LogState.I, format, args);
        }

        public void W(string format, params object[] args)
        {
            Output(LogState.W, format, args);
        }

        public void E(string format, params object[] args)
        {
            Output(LogState.E, format, args);
        }
        
        #endregion

        private void Output(LogState state, string format, params object[] args)
        {
            DateTime now = DateTime.Now;
            string message = args.Length == 0 ? format : string.Format(format, args);
            string output = string.Format("[{0}], [{1}] : {2}",
                now.ToString(), state.ToString(), message);

            Console.ForegroundColor = getForegroundColor(state);
            
            Console.WriteLine(output);

            Console.ResetColor();
        }

        private ConsoleColor getForegroundColor(LogState state)
        {
            ConsoleColor consoleColor = ConsoleColor.White;

            switch (state)
            {
                case LogState.T:
                    {
                        consoleColor = ConsoleColor.Gray;
                        break;
                    }
                case LogState.D:
                    {
                        consoleColor = ConsoleColor.Gray;
                        break;
                    }
                case LogState.I:
                    {
                        consoleColor = ConsoleColor.DarkGreen;
                        break;
                    }
                case LogState.W:
                    {
                        consoleColor = ConsoleColor.Yellow;
                        break;
                    }
                case LogState.E:
                    {
                        consoleColor = ConsoleColor.Red;
                        break;
                    }
            }

            return consoleColor;
        }

        #region ConsoleLog 測試輸出顏色

        public static void UT()
        {
            foreach (ConsoleColor consoleColor in Enum.GetValues(typeof(ConsoleColor)))
            {
                Console.WriteLine(consoleColor.ToString());

                Console.ForegroundColor = consoleColor;

                Console.WriteLine("This is a testing string !!");

                Console.ResetColor();
            }
        }

        #endregion


    } // end of ConsoleLog
}
