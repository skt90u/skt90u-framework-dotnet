using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using JUtil.Reflection;

namespace JUtil.Path
{
    /// <summary>
    /// Extensions of File Management
    /// </summary>
    public static class File
    {
        public static string GetParentDirectory(string filePath)
        {
            FileInfo fInfo = new FileInfo(filePath);

            return fInfo.Directory.Name;
        }

        public static string GetDirectory(string filePath)
        {
            // Path.GetDirectoryName(filePath)
            FileInfo fInfo = new FileInfo(filePath);

            return fInfo.Directory.ToString();
        }

        public static string GetFileName(string filePath)
        {
            FileInfo fInfo = new FileInfo(filePath);

            return fInfo.Name;
        }

        public static string GetContentString(string filepath)
        {
            return GetContentString(filepath, System.Text.Encoding.Default);
        }

        public static string GetContentString(string filepath, Encoding encoding)
        {
            string ret = string.Empty;

            // StreamReader　讀取中文亂碼解決方法
            // http://endlesslive.blogspot.com/2007/07/streamreader.html
            using (StreamReader reader = new StreamReader(filepath, encoding))
            {
                ret = reader.ReadToEnd();
            }

            return ret;
        }

        /// <summary>
        /// get content of a file
        /// </summary>
        public static List<string> GetContent(string filepath)
        {
            // StreamReader　讀取中文亂碼解決方法
            // http://endlesslive.blogspot.com/2007/07/streamreader.html
            return GetContent(filepath, System.Text.Encoding.Default);
        }

        public static List<string> GetContent(string filepath, Encoding encoding)
        {
            List<string> contents = new List<string>();

            // StreamReader　讀取中文亂碼解決方法
            // http://endlesslive.blogspot.com/2007/07/streamreader.html
            using (StreamReader reader = new StreamReader(filepath, encoding))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    contents.Add(line);
                }
            }

            return contents;
        }

        

        /// <summary>
        /// get content of a file
        /// </summary>
        public static List<string> GetContent(Stream stream)
        {
            List<string> contents = new List<string>();

            // StreamReader　讀取中文亂碼解決方法
            // http://endlesslive.blogspot.com/2007/07/streamreader.html
            using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    contents.Add(line);
                }
            }

            return contents;
        }

        /// <summary>
        /// determine a specified file whether exist
        /// </summary>
        public static bool Exists(string filepath)
        {
            FileInfo fileInfo = new FileInfo(filepath);
            return fileInfo.Exists;
        }

        /// <summary>
        /// copy a file
        /// </summary>
        public static void Copy(string from, string to)
        {
            System.IO.File.Copy(from, to);
        }

        /// <summary>
        /// delete a file
        /// </summary>
        public static void Delete(string filepath)
        {
            System.IO.File.Delete(filepath);
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
        /// get absolute path
        /// </summary>
        public static string GetAbsolutePath(string filename)
        {
            string curDirectory = JUtil.Path.Directory.Application;
            return GetAbsolutePath(curDirectory, filename);
        }
        /// <summary>
        /// View a file
        /// </summary>
        public static void Explore(string filepath)
        {
            if (Exists(filepath))
            {
                string explorer = GetExplorerPath();

                ProcessStartInfo pi = new ProcessStartInfo(explorer, filepath);

                System.Diagnostics.Process.Start(pi);
            }
        }

        enum ExplorePolicy
        {
            Notepad = 0,
            NotepadPlus = 1,
            BvEdit = 2
        }

        private static ExplorePolicy explorePolicy = ExplorePolicy.BvEdit;

        private static string GetExplorerPath()
        {
            string explorerPath = string.Empty;

            switch (explorePolicy)
            {
                case ExplorePolicy.Notepad:
                    {
                        explorerPath = @"C:\WINDOWS\notepad.exe";
                        break;
                    }
                case ExplorePolicy.NotepadPlus:
                    {
                        explorerPath = @"E:\tools\Notepad++\notepad++.exe";
                        break;
                    }
                case ExplorePolicy.BvEdit:
                    {
                        explorerPath = @"D:\BvEdit\BvEdit.exe";
                        break;
                    }
            }

            if (!Exists(explorerPath))
            {
                explorerPath = @"C:\WINDOWS\notepad.exe";
            }

            return explorerPath;
        }

    } //  end of File
}
