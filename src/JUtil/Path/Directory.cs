using System.Reflection;
using System.Diagnostics;
using System.IO;
using NativeApi = System.IO;
using System.Web.Hosting;

namespace JUtil.Path
{
    /// <summary>
    /// Extensions of Directory Management
    /// </summary>
    public static  class Directory
    {
        /// <summary>
        /// 取得目前執行檔所在目錄
        /// </summary>
        public static string Application
        {
            get
            {
                string path = string.Empty;

                if (application == null)
                {
                    if (Environment.ApplicationType == ApplicationType.WebApplication)
                    {
                        path = HostingEnvironment.MapPath("~/");
                    }
                    else
                    {
                        // WPF (How to get executable path)
                        // http://sne04.blogspot.com/2008/12/wpf-how-to-get-executable-path.html
                        // 雖然會得到的是目前 JUtil.dll 的路徑, 但是他會和執行檔放置在同一各目錄, 
                        // 因此與我們預期的結果相同
                        path = Assembly.GetExecutingAssembly().Location;

                        int pos = path.LastIndexOf('\\');
                        if (pos != -1)
                        {
                            path = path.Substring(0, pos);
                        }
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
            try
            {
                NativeApi.DirectoryInfo directoryInfo = new NativeApi.DirectoryInfo(directory);
                return directoryInfo.Exists;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// copy a directory
        /// </summary>
        public static void Copy(string from, string to)
        {
            string[] Files;

            if (to[to.Length - 1] != NativeApi.Path.DirectorySeparatorChar)
                to += NativeApi.Path.DirectorySeparatorChar;

            if (!Directory.Exists(to)) NativeApi.Directory.CreateDirectory(to);
            Files = NativeApi.Directory.GetFileSystemEntries(from);

            foreach (string Element in Files)
            {
                if (Directory.Exists(Element))
                {
                    Copy(Element, to + NativeApi.Path.GetFileName(Element));
                }
                else
                {
                    NativeApi.File.Copy(Element, to + NativeApi.Path.GetFileName(Element), true);
                }
            }
        }

        /// <summary>
        /// delete a folder
        /// </summary>
        public static void Delete(string directory)
        {
            System.IO.Directory.Delete(directory);
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

        public static string GetSpecialFolder(System.Environment.SpecialFolder specialFolder)
        {
            string path = System.Environment.GetFolderPath(specialFolder);

            MakeSureExists(path);
            
            return path;
        }
        
    } //  end of Directory
}
