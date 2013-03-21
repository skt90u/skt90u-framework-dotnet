using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace JUtil.Path
{
    public static  class Directory
    {
        public static string Application
        {
            get
            {
                if (application == null)
                {
                    // WPF (How to get executable path)
                    // http://sne04.blogspot.com/2008/12/wpf-how-to-get-executable-path.html
                    // 雖然會得到的是目前 IHLCD.Kernel.dll 的路徑, 但是他會和執行檔放置在同一各目錄, 
                    // 因此與我們預期的結果相同
                    string path = Assembly.GetExecutingAssembly().Location;

                    int pos = path.LastIndexOf('\\');
                    if (pos != -1)
                    {
                        path = path.Substring(0, pos);
                    }

                    application = path;
                }
                return application;
            }
        }
        private static string application;

        /// <summary>
        /// determine a specified directory whether exist
        /// </summary>
        public static bool Exists(string directory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            return directoryInfo.Exists;
        }

        /// <summary>
        /// make sure a directory exist, if not create it automatically
        /// </summary>
        /// <remarks>
        /// throw exception while create directory failed
        /// </remarks>
        public static void MakeSureExists(string directory)
        {
            if (!Exists(directory))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                directoryInfo.Create();
            }
        }

        /// <summary>
        /// Explore a directory
        /// </summary>
        public static void Explore(string directory)
        {
            if (Exists(directory))
            {
                string explorer = "explorer.exe";

                ProcessStartInfo pi = new ProcessStartInfo(explorer, directory);

                System.Diagnostics.Process.Start(pi);
            }
        }


    } //  end of Directory
}
