using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyJUtil
{
    public static class ExtPath
    {
        /// <summary>
        /// 合併成絕對路徑
        /// </summary>
        public static string GetAbsolutePath(string directory, string filename)
        {
            return directory.Trim('\\') + "\\" + filename.Trim('\\');
        }

        /// <summary>
        /// 將相對路徑轉成成絕對路徑
        /// </summary>
        public static string GetAbsolutePath(string filename)
        {
            return GetAbsolutePath(Environment.ApplicationDirectory, filename);
        }
    }
}
