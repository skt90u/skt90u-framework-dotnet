using System;
using System.IO;

namespace JUtil
{
    public class SetupDB
    {
        private IDatabase db;

        public SetupDB(IDatabase db)
        {
            this.db = db;
        }

        /// <remarks>
        /// (new SetupDB(db)).Install(filepath, "\r\nGO\r\n");
        /// </remarks>
        public void Install(string filepath, string splitter)
        {
            string commandText = string.Empty;

            using (StreamReader reader = new StreamReader(filepath, System.Text.Encoding.Default))
            {
                commandText = reader.ReadToEnd();
            }

            ExecuteCommandWithSplitter(commandText, splitter);
        }

        private void ExecuteCommandWithSplitter(string commandText, string splitter)
        {
            int startPos = 0;

            do
            {
                int lastPos = commandText.IndexOf(splitter, startPos);
                int len = (lastPos > startPos ? lastPos : commandText.Length) - startPos;
                string query = commandText.Substring(startPos, len);

                if (query.Trim().Length > 0)
                {
                    try
                    {
                        db.ExecuteSQL(query);
                    }
                    catch (Exception ex)
                    {
                        Log.E(ex);
                    }
                }

                if (lastPos == -1)
                    break;
                else
                    startPos = lastPos + splitter.Length;
            } while (startPos < commandText.Length);

        }
    }
}
