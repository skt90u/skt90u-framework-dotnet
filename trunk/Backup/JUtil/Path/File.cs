using System.IO;
using System.Diagnostics;

namespace JUtil.Path
{
    public static class File
    {
        /// <summary>
        /// determine a specified file whether exist
        /// </summary>
        public static bool Exists(string filepath)
        {
            FileInfo fileInfo = new FileInfo(filepath);
            return fileInfo.Exists;
        }

        /// <summary>
        /// get absolute path
        /// </summary>
        public static string GetAbsolutePath(string directory, string filename)
        {
            string path = directory;

            if (!path.EndsWith("\\"))
                path += "\\";

            path += filename;

            return path;
        }

        /// <summary>
        /// View a file
        /// </summary>
        public static void Explore(string filepath)
        {
            if (Exists(filepath))
            {
                string notepad = @"C:\WINDOWS\notepad.exe";
                string notepadPlus = @"E:\tools\Notepad++\notepad++.exe";

                string explorer = Exists(notepadPlus) ? notepadPlus : notepad;

                ProcessStartInfo pi = new ProcessStartInfo(explorer, filepath);

                System.Diagnostics.Process.Start(pi);
            }
        }


    } //  end of File
}
