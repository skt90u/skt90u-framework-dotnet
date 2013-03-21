using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUtil
{
    /// <summary>
    /// an implementation of IExtMessageBox interface in console platform
    /// </summary>
    public class ConsoleMessageBox : IExtMessageBox
    {
        #region IMessageBox 成員

        /// <summary>
        /// show error MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public void Error(string text, string caption)
        {
            Output(ExtMessageBoxCategory.Error, text, caption);
        }

        /// <summary>
        /// show error MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public void Info(string text, string caption)
        {
            Output(ExtMessageBoxCategory.Info, text, caption);
        }

        /// <summary>
        /// Output specified message with different color according to its ExtMessageBoxCategory
        /// </summary>
        /// <param name="category"></param>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        private void Output(ExtMessageBoxCategory category, string text, string caption)
        {
            Console.ForegroundColor = getForegroundColor(category);

            string msg = string.Format("[{0}] : {1}", caption, text);
            Console.WriteLine(msg);

            Console.ResetColor();
        }

        /// <summary>
        /// get ForegroundColor color according to its ExtMessageBoxCategory
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private ConsoleColor getForegroundColor(ExtMessageBoxCategory category)
        {
            ConsoleColor consoleColor = ConsoleColor.White;

            switch (category)
            {
                case ExtMessageBoxCategory.Error:
                    {
                        consoleColor = ConsoleColor.Red;
                        break;
                    }
                case ExtMessageBoxCategory.Info:
                    {
                        consoleColor = ConsoleColor.Yellow;
                        break;
                    }
            }

            return consoleColor;
        }
        #endregion
    }
}
