using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using JUtil.Reflection;

namespace JUtil
{
    public class LiveUpdate
    {
        private string ExecutablePath
        {
            get { return System.Windows.Forms.Application.ExecutablePath; }
        }

        private static bool needUpdate;
        public static bool NeedUpdate
        {
            get { return needUpdate; }
        }

        private string appcastUrl;

        public LiveUpdate(string appcastUrl)
        {
            this.appcastUrl = appcastUrl;
        }

        /*
        public void CheckUpdate()
        {
            AppCast appCast = new AppCast(appcastUrl);

            AppCastItem latestVersion = appCast.GetLatestVersion();

            if (IsUpdateRequired(latestVersion))
                DoUpdate(latestVersion);
        }
        */

        public bool IsUpdateRequired()
        {
            AppCast appCast = new AppCast(appcastUrl);

            AppCastItem latestVersion = appCast.GetLatestVersion();

            return IsUpdateRequired(latestVersion);
        }

        public void DoUpdate()
        {
            AppCast appCast = new AppCast(appcastUrl);

            AppCastItem latestVersion = appCast.GetLatestVersion();

            if (IsUpdateRequired(latestVersion))
                DoUpdate(latestVersion);
        }

        private bool IsUpdateRequired(AppCastItem latestVersion)
        {
            if (latestVersion == null) return false;

            Version v1 = new Version(InstalledVersion);
            Version v2 = new Version(latestVersion.Version);

            return v2 > v1;
        }

        /// <summary>
        /// 取得安裝的組件版本
        /// </summary>
        public string InstalledVersion
        {
            get
            {
                if(installedVersion == null)
                {
                    AssemblyAccessor accessor = new AssemblyAccessor(ExecutablePath);

                    installedVersion = accessor.AssemblyVersion;
                }

                return installedVersion;
            }
        }
        private string installedVersion;

        private void DoUpdate(AppCastItem latestVersion)
        {
            if (latestVersion == null) return;

            string downloadLink = latestVersion.DownloadLink;

            if (!WebFileExist(downloadLink)) return;

            LaunchLiveUpdater(downloadLink, ExecutablePath);

            // close current program
            ShutDown();
        }

        private bool WebFileExist(string url)
        {
            try
            {
                WebRequest req = HttpWebRequest.Create(url);
                Stream ret = req.GetResponse().GetResponseStream();
                ret.Close();
                return true;
            }
            catch (Exception ex)
            {
                string error = string.Format("WebFileExist(url = {0}) : {1}", url, ex.Message);
                Log.E(new Exception(error));
                return false;
            }
        }

        private static void LaunchLiveUpdater(string downloadLink, string calleePath)
        {
            string filepath = JUtil.Path.File.GetAbsolutePath(JUtil.Path.Directory.Application, @"LiveUpdater.exe");

            if (!JUtil.Path.File.Exists(filepath))
            {
                string error = string.Format("無法更新程式，找不到 {0}", filepath);
                throw new Exception(error);
            }

            string arguments = string.Format("\"{0}\" \"{1}\"", downloadLink, calleePath);

            JUtil.Process.Run(filepath, arguments);
        }

        private static void ShutDown()
        {
            LiveUpdate.needUpdate = true;

            // for wpf
            //WPFWindow.Application.Current.Shutdown();

            // for winform
            System.Windows.Forms.Application.Exit();
        }
    }
}
